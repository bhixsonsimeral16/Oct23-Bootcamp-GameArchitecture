using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject obejectToBuild;
    [SerializeField] private Transform buildPoint;

    public void Build()
    {
        Instantiate(obejectToBuild, buildPoint.position, buildPoint.rotation);
        Destroy(gameObject);
    }
}
