using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour, IDestroyable
{
    public void OnCollided()
    {
        Destroy(gameObject);
    }
}
