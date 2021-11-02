using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public Stack<GameObject> pooledObjects;
    //public List<GameObject> pooledObjects;
    public int poolSize;

    public void SetupPool()
    {
        pooledObjects = new Stack<GameObject>(poolSize);
        for (int i = poolSize; i > 0; i--)
        {
            InstantiateObject();
        }

    }

    public void InstantiateObject()
    {
        GameObject obj = Instantiate(objectToPool, transform);
        pooledObjects.Push(obj);
      //  pooledObjects.Add(obj);
        obj.SetActive(false);
    }

    public GameObject GetFromPool()
    {
        if (pooledObjects.Count > 0)
        {
            // GameObject returnedObject = pooledObjects[0];
            GameObject returnedObject = pooledObjects.Pop();
            returnedObject.SetActive(true);
            //pooledObjects.Remove(returnedObject);
            return returnedObject;


        }
        else
        {
            Debug.Log("Object not available, instantiating a new one");
            GameObject obj = Instantiate(objectToPool, transform);
            obj.SetActive(true);
            return obj;

        }
    }

    public void ReturnToPool(GameObject returningObject)
    {
        pooledObjects.Push(returningObject);
        //pooledObjects.Add(returningObject);
        returningObject.SetActive(false);

    }
}
