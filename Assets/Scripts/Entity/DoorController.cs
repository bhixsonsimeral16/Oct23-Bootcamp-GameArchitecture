using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // [SerializeField] private MeshRenderer doorRenderer;
    // [SerializeField] private Material defaultMaterial;
    // [SerializeField] private Material triggeredMaterial;
    [SerializeField] private Animator doorAnimator;
    
    bool isLocked = true;
    float timer = 0f;
    const float WAIT_TIME = 1.0f;
    const string DOOR_PARAM = "Door";
    
    void OnTriggerEnter(Collider other)
    {
        if (isLocked || !other.gameObject.CompareTag("Player"))
        {
            return;
        }

        timer = 0f;
        // doorRenderer.material = triggeredMaterial;
    }

    void OnTriggerStay(Collider other)
    {
        if (isLocked || !other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        timer += Time.deltaTime;
        if (timer >= WAIT_TIME)
        {
            timer = WAIT_TIME;

            // Open the door
            // doorAnimator.SetBool(DOOR_PARAM, true);
            OpenDoor(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Player exited door trigger");
        timer = 0f;
        // doorRenderer.material = defaultMaterial;

        // Close the door
        // doorAnimator.SetBool(DOOR_PARAM, false);
        OpenDoor(false);
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void OpenDoor(bool isOpen)
    {
        if(!isLocked)
        {
            doorAnimator.SetBool(DOOR_PARAM, isOpen);
        }
    }
}
