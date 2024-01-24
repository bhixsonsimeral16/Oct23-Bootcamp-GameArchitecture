using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : Interactor
{
    [SerializeField] private float jumpVelocity;

    private PlayerMovement playerMovement;

    public override void Interact()
    {
        if(playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if (playerInput.jumpPressed && playerMovement.isGrounded)
        {
            playerMovement.SetYVelocity(jumpVelocity);
        }
    }
}
