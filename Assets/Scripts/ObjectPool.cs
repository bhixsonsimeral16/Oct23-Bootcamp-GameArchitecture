using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public int startSize;

    [SerializeField] List<PooledObject> objectPool = new List<PooledObject>();
    [SerializeField] List<PooledObject> activeObjects = new List<PooledObject>();

    PooledObject tempObject;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < startSize; i++)
        {
            AddNewObject();
        }
    }

    void AddNewObject()
    {
        tempObject = Instantiate(objectToPool, transform).GetComponent<PooledObject>();
        tempObject.gameObject.SetActive(false);
        tempObject.SetObjectPool(this);
        objectPool.Add(tempObject);
    }

    public PooledObject GetPooledObject()
    {
        PooledObject tempObj;
        if(objectPool.Count > 0)
        {
            tempObj = objectPool[0];
            activeObjects.Add(tempObj);
            objectPool.RemoveAt(0);
        }
        else
        {
            // Give more objects upon request
            AddNewObject();
            tempObj = GetPooledObject();
        }

        tempObj.gameObject.SetActive(true);
        return tempObj;
    }

    public void DestroyPooledObject(PooledObject obj, float time = 0)
    {
        if (time == 0)
        {
            obj.Destroy();
        }
        else
        {
            obj.Destroy(time);
        }
    }

    public void RestoreObject(PooledObject obj)
    {
        Debug.Log("Restore");

        obj.gameObject.SetActive(false);
        activeObjects.Remove(obj);
        objectPool.Add(obj); 
    }
}
