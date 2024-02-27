using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    
    bool isLocked = true;
    float timer = 0f;
    const float WAIT_TIME = 0.5f;
    const string DOOR_PARAM = "Door";
    
    void OnTriggerEnter(Collider other)
    {
        if (isLocked || !other.gameObject.CompareTag("Player"))
        {
            return;
        }

        timer = 0f;
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

        // Close the door
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
