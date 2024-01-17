using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask holdableLayerMask;

    public UnityEvent OnCubePlaced;
    public UnityEvent OnCubeRemoved;

    bool isActivated = false;
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Pressure Pad Collision Enter");
        // Is cube close to center of pressure pad?
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, holdableLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("PickupCube"))
            {
                isActivated = true;
                OnCubePlaced.Invoke();
                break;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        // Following code only works if there is only ever one PickupCube in the scene
        // if (other.gameObject.CompareTag("PickupCube"))
        // {
        //     OnCubeRemoved.Invoke();
        // }

        // Return is not active or the object is not a cube
        if (!isActivated || !other.gameObject.CompareTag("PickupCube")) return;

        isActivated = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, holdableLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("PickupCube"))
            {
                isActivated = true;
                return;
            }
        }

        OnCubeRemoved.Invoke();
    }
}
