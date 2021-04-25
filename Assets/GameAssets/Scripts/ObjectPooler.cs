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
        public int actualAlive;
        public float timeToSpawn = 20;
        public float RandomRangePosition;
        public float actualTime = 0;
        public int algo = 0;
    }
    #region Singleton
    private static ObjectPooler Instance;
    public static ObjectPooler Get()
    {
        return Instance;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                MeshRenderer  mesh =obj.GetComponent<MeshRenderer>();
                //mesh.enabled = false;
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    private void Update()
    {
        foreach (Pool pool in pools)
        {
            if(pool.prefab!=null)
            {
                if(pool.actualTime < pool.timeToSpawn)
                {
                    pool.actualTime += Time.deltaTime;
                }
                else if(pool.actualTime >= pool.timeToSpawn && pool.actualAlive < pool.size)
                {
                    SpawnFromPool(pool.tag, new Vector3(Random.Range(-pool.RandomRangePosition, pool.RandomRangePosition), 1, Random.Range(-pool.RandomRangePosition, pool.RandomRangePosition)), Quaternion.identity);
                    pool.actualTime = 0;
                    pool.actualAlive++;
                    pool.algo++;
                    Debug.Log(pool.algo);
                }
            }
        }
    }
    private void SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't excist");
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        MeshRenderer mesh = objectToSpawn.GetComponent<MeshRenderer>();
        
        //mesh.enabled = true;
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        IObjectPooler pooledObj = objectToSpawn.GetComponent<IObjectPooler>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);
    } 
}
