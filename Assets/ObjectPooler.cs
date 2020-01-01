using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool canExpand;
    }

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
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool p in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < p.size; i++)
            {
                GameObject obj = Instantiate(p.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(p.tag, objectPool);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        //get pool (class) that this object is derived from
        Pool thisObjPool = pools.Find(x => x.tag == tag);

        foreach (GameObject g in poolDictionary[tag])
        {
            if (!g.activeSelf)
            {
                return g;
            }
        }

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

    public void SpawnPooledObject(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject objToSpawn = GetPooledObject(tag);

        if (objToSpawn != null)
        {
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;
            objToSpawn.SetActive(true);
        }
    }

    public void DespawnObjectIntoPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
