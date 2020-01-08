using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton

    public static ObjectPooler instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        //create dictionary to store pools
        poolDictionary = new Dictionary<string, List<GameObject>>();

        //create the pools using "pools" list and adding them to the dictionary
        foreach (Pool p in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            //instantiate pools.size number of pools.prefabs
            for (int i = 0; i < p.size; i++)
            {
                GameObject obj = Instantiate(p.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            //add the list of object to the dictionary
            poolDictionary.Add(p.tag, objectPool);
        }
    }

    /// <summary>
    /// returns any pooled object with specified tag
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public GameObject GetPooledObject(string tag)
    {
        //get pool (class) that this object is derived from
        Pool thisObjPool = pools.Find(x => x.tag == tag);

        //return first gameObject that is inactive
        foreach (GameObject g in poolDictionary[tag])
        {
            if (!g.activeSelf)
            {
                return g;
            }
        }

        //if all gameObjects with the tag are active already
        //instantiates a new object and adds it to the pool
        //return the newly instantiated object
        if (thisObjPool.canExpand)
        {
            GameObject newObj = (GameObject)Instantiate(thisObjPool.prefab);
            newObj.SetActive(false);
            poolDictionary[tag].Add(newObj);
            return newObj;
        }
        else
        {
            return null;
        }

    }

    /// <summary>
    /// Spawns a pooled object of specified tag, with specified position and rotation
    /// optional parameter to pass in the pooled object to spawn
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="objToSpawnParameter"></param>
    /// <returns></returns>
    public GameObject SpawnPooledObject(string tag, Vector3 position, Quaternion rotation, GameObject objToSpawnParameter = null)
    {
        GameObject objToSpawn;

        //set objToSpawn depending on objToSpawnParameter
        if (objToSpawnParameter == null)
        {
             objToSpawn = GetPooledObject(tag);
        }
        else
        {
            objToSpawn = objToSpawnParameter;
        }

        //set position and rotation
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        //activate object
        objToSpawn.SetActive(true);

        return objToSpawn;
    }

    //despawns object by deactivating it
    public void DespawnObjectIntoPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
