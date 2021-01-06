using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	Vector2 worldSize = new Vector2(40, 40);

	[Header("Rooms")]
	public Room[,] rooms;
	public int roomWidth;
	public int roomHeight;
	public RoomGenerator roomGenerator;

	[Header("Grid Values")]
	List<Vector2> takenPositions = new List<Vector2>();
	public Vector2Int startRoomPos = new Vector2Int(0, 0);
	int gridSizeX, gridSizeY;
	public int numberOfRooms = 20, distanceBetweenRooms;

	void Start()
	{

		if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
		{ // make sure we dont try to make more rooms than can fit in our grid
			numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
		}
		gridSizeX = Mathf.RoundToInt(worldSize.x); //note: these are half-extents
		gridSizeY = Mathf.RoundToInt(worldSize.y);
		CreateRooms(); //lays out the actual map
		//PlaceRoomGraphicAndColliders();
	}

	public void PlaceRoomGraphicAndColliders()
	{
		foreach (Room r in rooms)
		{
			Vector3Int thisRoomPos = new Vector3Int((int)r.gridPos.x, (int)r.gridPos.x, 0);
			roomGenerator.PlaceRoom(thisRoomPos, roomWidth, roomHeight);
		}
	}

	void CreateRooms()
	{
		//setup
		rooms = new Room[gridSizeX * 2, gridSizeY * 2];
		rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
		Vector3Int thisRoomPos = new Vector3Int(startRoomPos.x, startRoomPos.y, 0);
		roomGenerator.PlaceRoom(thisRoomPos, roomWidth, roomHeight);
		takenPositions.Insert(0, Vector2.zero);
		Vector2 checkPos = Vector2.zero;
		//magic numbers
		float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
		//add rooms
		for (int i = 0; i < numberOfRooms - 1; i++)
		{
			float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
			randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
			//grab new position
			checkPos = NewPosition();
			//test new position
			if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
			{
				int iterations = 0;
				do
				{
					checkPos = SelectiveNewPosition();
					iterations++;
				} while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
				if (iterations >= 50)
					print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
			}
			//finalize position
			rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
			thisRoomPos = new Vector3Int((int)(checkPos.x * roomWidth + checkPos.x * (distanceBetweenRooms + 2)) + startRoomPos.x, (int)(checkPos.y * roomHeight + checkPos.y * (distanceBetweenRooms + 2)) + startRoomPos.y, 0);
			roomGenerator.PlaceRoom(thisRoomPos, roomWidth, roomHeight);
			takenPositions.Insert(0, checkPos);
		}
	}

	Vector2 NewPosition()
	{
		int x = 0, y = 0;
		Vector2 checkingPos = Vector2.zero;
		do
		{
			int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
			x = (int)takenPositions[index].x;//capture its x, y position
			y = (int)takenPositions[index].y;
			bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
			bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
			if (UpDown)
			{ //find the position bnased on the above bools
				if (positive)
				{
					y += 1;
				}
				else
				{
					y -= 1;
				}
			}
			else
			{
				if (positive)
				{
					x += 1;
				}
				else
				{
					x -= 1;
				}
			}
			checkingPos = new Vector2(x, y);
		} while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
		return checkingPos;
	}

	Vector2 SelectiveNewPosition()
	{ // method differs from the above in the two commented ways
		int index = 0, inc = 0;
		int x = 0, y = 0;
		Vector2 checkingPos = Vector2.zero;
		do
		{
			inc = 0;
			do
			{
				//instead of getting a room to find an adject empty space, we start with one that only 
				//as one neighbor. This will make it more likely that it returns a room that branches out
				index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
				inc++;
			} while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
			x = (int)takenPositions[index].x;
			y = (int)takenPositions[index].y;
			bool UpDown = (Random.value < 0.5f);
			bool positive = (Random.value < 0.5f);
			if (UpDown)
			{
				if (positive)
				{
					y += 1;
				}
				else
				{
					y -= 1;
				}
			}
			else
			{
				if (positive)
				{
					x += 1;
				}
				else
				{
					x -= 1;
				}
			}
			checkingPos = new Vector2(x, y);
		} while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
		if (inc >= 100)
		{ // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
			print("Error: could not find position with only one neighbor");
		}
		return checkingPos;
	}

	int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
	{
		int ret = 0; // start at zero, add 1 for each side there is already a room
		if (usedPositions.Contains(checkingPos + Vector2.right))
		{ //using Vector.[direction] as short hands, for simplicity
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.left))
		{
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.up))
		{
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.down))
		{
			ret++;
		}
		return ret;
	}

	
}
