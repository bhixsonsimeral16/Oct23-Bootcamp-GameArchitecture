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
        // Is cube close to center of pressure pad?
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, holdableLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("PickupCube"))
            {
                Debug.Log("Cube placed on pressure pad");
                isActivated = true;
                OnCubePlaced.Invoke();
                break;
            }
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("PickupCube")) return;

        // Is cube close to center of pressure pad?
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, holdableLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("PickupCube"))
            {
                if (!isActivated)
                {
                    Debug.Log("Cube shifted onto pressure pad");
                    OnCubePlaced.Invoke();
                }
                isActivated = true;
                return;
            }
        }

        Debug.Log("Cube shifted, but is not on pressure pad");
        OnCubeRemoved.Invoke();
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
            if (collider.CompareTag("PickupCube"))
            {
                Debug.Log("Cube is still on pressure pad");
                isActivated = true;
                return;
            }
        }

        OnCubeRemoved.Invoke();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
