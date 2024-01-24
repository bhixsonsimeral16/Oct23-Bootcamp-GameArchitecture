using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBehaviour : MonoBehaviour
{
    [SerializeField] private float turnSpeed;

    private PlayerInput playerInput;
    
    void Start()
    {
        playerInput = PlayerInput.GetInstance();
    }
    
    void Update()
    {
        TurnPlayer();
    }

    void TurnPlayer()
    {
        transform.Rotate(Vector3.up * playerInput.mouseXInput * turnSpeed * Time.deltaTime);
    }
}
