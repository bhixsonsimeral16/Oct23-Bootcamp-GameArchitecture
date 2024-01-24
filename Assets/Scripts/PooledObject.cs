using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool associatedPool;

    private float timer;
    private bool setToDestroy = false;
    private float timeToDestroy = 0f;

    void Update()
    {
        if (setToDestroy)
        {
            timer += Time.deltaTime;
            if (timer >= timeToDestroy)
            {
                timer = 0;
                setToDestroy = false;
                Destroy();
            }
        }
    }

    public void SetObjectPool(ObjectPool pool)
    {
        associatedPool = pool;
        timer = 0;
        timeToDestroy = 0;
        setToDestroy = false;
    }

    public void Destroy()
    {
        if (associatedPool != null)
        {
            associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time)
    {
        setToDestroy = true;
        timeToDestroy = time;
    }
}
