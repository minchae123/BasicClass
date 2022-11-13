using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int poolSize = 10;

    protected Queue<GameObject> objectPool;

    public Transform spawnedObjectParent;

    private void Awake()
    {
        objectPool = new Queue<GameObject> ();
    }

    public void InitialLized(GameObject obj, int size = 10)
    {
        objectToPool = obj;
        poolSize = size;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnObj = null;
        if(objectPool.Count < poolSize)
        {
            spawnObj = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnObj.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnObj.transform.SetParent(spawnedObjectParent);
        }
        else
        {
            spawnObj = objectPool.Dequeue();
            spawnObj.transform.position = transform.position;
            spawnObj.transform.rotation = Quaternion.identity;
            spawnObj.SetActive(true);
        }

        objectPool.Enqueue(spawnObj);
        return spawnObj;
    }

    private void CreateObjectParentIfNeeded()
    {
        if(spawnedObjectParent == null)
        {
            string name = "ObjectPool_" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null)
            {
                spawnedObjectParent = parentObject.transform;
            }
            else
            {
                spawnedObjectParent = new GameObject(name).transform;
            }
        }
    }
}
