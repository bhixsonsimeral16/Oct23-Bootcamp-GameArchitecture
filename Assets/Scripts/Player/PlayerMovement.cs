using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float sprintMultiplier;

    [Header("Player Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;

    private PlayerInput playerInput;
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private float moveMultiplier = 1f;
    public bool isGrounded { get; private set; }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = PlayerInput.GetInstance();
    }

    void Update()
    {
        PlayerGrounded();
        MovePlayer();
    }

    void MovePlayer()
    {
        moveMultiplier = playerInput.sprintHeld ? sprintMultiplier : 1f;

        characterController.Move((transform.forward * playerInput.verticalInput + transform.right * playerInput.horizontalInput) * moveSpeed * moveMultiplier * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void PlayerGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask);
    }

    public void SetYVelocity(float yVelocity)
    {
        playerVelocity.y = yVelocity;
    }

    public float GetForwardVelocity()
    {
        return playerInput.verticalInput * moveSpeed * moveMultiplier;
    }
}
