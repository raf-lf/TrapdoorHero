using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public int copies = 1;
    public int timesToRun = 1;
    //public Dictionary<GameObject, float> table = new Dictionary<GameObject, float>();

    public GameObject[] objectTable = new GameObject[0];
    public float[] objectWeight = new float[0];
    private float[] objectDrawWeight;


    protected virtual void Start()
    {
        Activate();

    }

    public virtual void Activate()
    {
        objectDrawWeight = new float[objectWeight.Length];

        for (int i = timesToRun; i > 0; i--)
            Spawn(Draw());

        Destroy(gameObject, 5);

    }

    private int Draw()
    {

        float weightSum = 0;

        for (int i = 0; i < objectWeight.Length; i++)
        {
            weightSum += objectWeight[i];

            objectDrawWeight[0] = objectWeight[0];
            if (i > 0) objectDrawWeight[i] = objectDrawWeight[i - 1] + objectWeight[i];

        }

        float roll = Random.Range(0, weightSum);

        int drawedObjectId = 0;

        for (int i = 0; i < objectTable.Length; i++)
        {
            if (roll < objectDrawWeight[i])
            {
                drawedObjectId = i;
                break;
            }

        }
        //Debug.Log("Rolled " + roll + ", drawing '" + objectTable[drawedObjectId].name + "'");

        return drawedObjectId;


    }

    private void Spawn(int objectId)
    {
        for (int i = copies; i > 0; i--)
        {
            if (objectTable[i] != null)
            {
                GameObject spawnedObject = Instantiate(objectTable[objectId], transform);
                spawnedObject.transform.position += new Vector3(Random.Range(-.5f,.5f), Random.Range(-.5f, .5f),0);
                spawnedObject.transform.parent = transform.parent;
            }
        }

    }
}
