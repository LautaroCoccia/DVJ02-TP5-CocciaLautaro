using System.Collections.Generic;
using UnityEngine;
public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public float health;
        public string tag;
        public GameObject prefab;
        public int size;
        public int actualAlive;
        public float timeToSpawn = 20;
        public float RandomRangePosition;
        public float actualTime = 0;
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
                pool.actualTime = pool.timeToSpawn;
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.gameObject.GetComponent<IPoolValuesInitializer>().SetHealth(pool.health);
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
                    SpawnFromPool(pool.tag, new Vector3(Random.Range(-pool.RandomRangePosition, pool.RandomRangePosition), 1, Random.Range(-pool.RandomRangePosition, pool.RandomRangePosition)), Quaternion.identity,pool );

                    pool.actualTime = 0;
                    pool.actualAlive++;
                }
            }
        }
    }
    private void SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation,Pool _pool)
    {
        if(!poolDictionary.ContainsKey(_tag))
        {
            Debug.LogWarning("Pool with tag " + _tag + " doesn't excist");
        }
        GameObject objectToSpawn = poolDictionary[_tag].Dequeue();
        objectToSpawn.transform.position = _position;
        objectToSpawn.transform.rotation = _rotation;
        objectToSpawn.transform.gameObject.GetComponent<IPoolValuesInitializer>().SetHealth(_pool.health);
        objectToSpawn.SetActive(true);
        IObjectPooler pooledObj = objectToSpawn.GetComponent<IObjectPooler>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        poolDictionary[_tag].Enqueue(objectToSpawn);
    }
    public void OnDie(GameObject _damageable)
    {
        foreach (Pool pool in pools)
        {
            if(pool.tag == _damageable.tag)
            {
                pool.actualAlive--;
            }
        }
    }
}
