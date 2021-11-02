using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public ObjectPool poolPrefab;
    public List<ObjectPool> poolList = new List<ObjectPool>();

    private void Awake()
    {
        GameManager.scriptPool = this;
    }

    public ObjectPool RequestPool(GameObject objectRequesting, int poolSize)
    {
        if (poolList.Count > 0)
        {
            bool poolFound = false;
            foreach (ObjectPool pool in poolList)
            {
                if (pool.objectToPool.name == objectRequesting.name)
                {
                    poolFound = true;
                    return pool;

                }

            }

            if (!poolFound)
                return SetupNewPool(objectRequesting, poolSize);
            else 
                return null;


        }
        else 
            return SetupNewPool(objectRequesting, poolSize);

    }

    public ObjectPool SetupNewPool(GameObject objectRequesting, int poolSize)
    {
        ObjectPool newPool = Instantiate(poolPrefab, transform);
        newPool.objectToPool = objectRequesting;
        newPool.poolSize = poolSize;
        poolList.Add(newPool);
        newPool.name = "Pool: " + objectRequesting.name;
        newPool.SetupPool();
        Debug.Log("Created an object pool of " + poolSize + " " + objectRequesting.name);
        return newPool;

    }


}
