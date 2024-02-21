using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{
    /*
    *   Turret will check for the player and transition to attack state if player
    *   is hit by the turret's line of sight.
    *   While in idle state, the turret will rotate to the left and right 
    *   to a variable degree.
    */
    bool rotateRight = true;
    public TurretIdleState(TurretController turret) : base(turret)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Turret is in Idle");
        turret.laser.enabled = true;
    }

    public override void OnStateExit()
    {
        // Play sound?
    }

    public override void OnStateUpdate()
    {
        RotateTurret();
        ShootLaser();
    }

    private void RotateTurret()
    {
        float bodyRotation = turret.turretBody.transform.localEulerAngles.y;
        if (rotateRight)
        {
            turret.turretBody.transform.Rotate(Time.deltaTime * turret.roationSpeed * Vector3.up);
            // Subtract 5 from the rotation degree to account for the float precision error
            if (bodyRotation >= turret.rotationDegree && bodyRotation < 360 - turret.rotationDegree - 5)
            {
                rotateRight = false;
            }
        }
        else
        {
            turret.turretBody.transform.Rotate(Time.deltaTime * turret.roationSpeed * Vector3.down);
            // Add 5 to the rotation degree to account for the float precision error
            if (bodyRotation > turret.rotationDegree + 5 && bodyRotation <= 360 - turret.rotationDegree)
            {
                rotateRight = true;
            }
        }
    }

    private void ShootLaser()
    {
        if (Physics.Raycast(turret.laserOrigin.position, turret.laserOrigin.forward, out RaycastHit hit, turret.laserRange))
        {
            turret.laser.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found");
                turret.player = hit.transform;
                turret.ChangeState(new TurretAttackState(turret));
            }
        }
        else
        {
            turret.laser.SetPosition(1, turret.laserOrigin.position + (turret.laserOrigin.forward * turret.laserRange));
        }
    }
}
