using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    [SerializeField] float liftHeight = 5f;
    [SerializeField] float liftSpeed = 1f;
    [SerializeField] bool isUp = false;

    bool isMoving = false;
    Vector3 targetPosition;


    void Update()
    {
        if(isMoving)
        {
            MoveLift();
        }
    }
    public void ToggleLift()
    {
        if (isMoving)
        {
            return;
        }

        if (isUp)
        {
            targetPosition = transform.position + Vector3.down * liftHeight;
        }
        else
        {
            targetPosition = transform.position + Vector3.up * liftHeight;
        }
        isMoving = true;
    }

    void MoveLift()
    {
        // Different movement systems for the lift
        // 2 options: Lerp or MoveTowards
        // Lerp appears to be much faster, but slows down near the end
        // MoveTowards is slower, but more consistent
        transform.position = Vector3.Lerp(transform.position, targetPosition, liftSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, liftSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
            isUp = !isUp;
        }
    }
}
