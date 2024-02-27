using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask activatorLayerMask;
    [SerializeField] string activatorTag;

    public UnityEvent OnCubePlaced;
    public UnityEvent OnCubeRemoved;

    bool isActivated = false;
    void OnTriggerEnter(Collider other)
    {
        // Is cube close to center of pressure pad?
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, activatorLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(activatorTag))
            {
                Debug.Log("Activator placed on pressure pad");
                isActivated = true;
                OnCubePlaced.Invoke();
                break;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag(activatorTag)) return;

        // Is cube close to center of pressure pad?
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, activatorLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(activatorTag))
            {
                if (!isActivated)
                {
                    Debug.Log("Activator shifted onto pressure pad");
                    OnCubePlaced.Invoke();
                }
                isActivated = true;
                return;
            }
        }
        OnCubeRemoved.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        // Return if not active or the object is not a cube
        if (!isActivated || !other.gameObject.CompareTag(activatorTag)) return;

        isActivated = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, activatorLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(activatorTag))
            {
                Debug.Log("Activator is still on pressure pad");
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
