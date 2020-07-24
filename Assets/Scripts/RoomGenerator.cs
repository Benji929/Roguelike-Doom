using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    [SerializeField] private List<Tile> floorTiles;
    [SerializeField] private List<Tile> wallTiles;
    //private List<>

    public IAstarAI Ai;
    private AstarPath activeAstarPath;

    public Vector3Int Center;
    public int width;
    public int height;

    private void Awake()
    {
        activeAstarPath = AstarPath.active;
        PlaceRoom(Center, width, height);
    }

    private void Start()
    {
        


    }

    private void Update()
    {
        //scan the graph the frame immediately after start frame (i.e. second frame of the scene loading)
        if (Time.time == Time.deltaTime)
        {
            activeAstarPath.Scan();
        }

    }

    public void PlaceRoom(Vector3Int center, int width, int height)
    {
        PlaceRoomGroundTiles(center, width, height);
        PlaceRoomWallTiles(center, width, height);
    }

    public void PlaceRoomGroundTiles(Vector3Int center, int roomWidth, int roomHeight)
    {
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                int randInt = Random.Range(0, floorTiles.Count);
                floorTilemap.SetTile(center + new Vector3Int(roomWidth / 2 - x, roomHeight / 2 - y, 0), floorTiles[randInt]);
            }
        }
    }

    public void PlaceRoomWallTiles(Vector3Int center, int roomWidth, int roomHeight)
    {
        for (int x = -1; x < roomWidth + 1; x++)
        {
            int randInt = Random.Range(0, wallTiles.Count);
            wallTilemap.SetTile(center + new Vector3Int(roomWidth / 2 - x, roomHeight / 2 + 1, 0), wallTiles[randInt]);
            randInt = Random.Range(0, wallTiles.Count);
            wallTilemap.SetTile(center + new Vector3Int(roomWidth / 2 - x, roomHeight / 2 - roomHeight, 0), wallTiles[randInt]);
        }

        for (int y = 0; y < roomHeight; y++)
        {
            int randInt = Random.Range(0, wallTiles.Count);
            wallTilemap.SetTile(center + new Vector3Int(roomWidth / 2 + 1, roomHeight / 2 - y, 0), wallTiles[randInt]);
            randInt = Random.Range(0, wallTiles.Count);
            wallTilemap.SetTile(center + new Vector3Int(roomWidth / 2 - roomWidth, roomHeight / 2 - y, 0), wallTiles[randInt]);
        }
    }
}
