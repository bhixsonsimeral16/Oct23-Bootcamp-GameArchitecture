using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCube : MonoBehaviour, IHoldable
{
    Rigidbody cubeRigidbody;

    void Start()
    {
        cubeRigidbody = GetComponent<Rigidbody>();
    }
    
    public void OnHold(Transform attachTransform)
    {
        transform.SetPositionAndRotation(attachTransform.position, attachTransform.rotation);
        transform.SetParent(attachTransform);

        cubeRigidbody.isKinematic = true;
        cubeRigidbody.useGravity = false;
    }

    public void OnRelease()
    {
        transform.SetParent(null);

        cubeRigidbody.isKinematic = false;
        cubeRigidbody.useGravity = true;
    }
}
