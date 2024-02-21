using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    /*
    *   Every second the turret will damage the player if
    *   the player is within the turret's laser (raycast).
    *   If the player is not within the turret's laser, the turret
    *   will check a spherecast to see if the player is within the
    *   turret's line of sight. If the player is within the turret's
    *   line of sight, the turret will rotate at a designated speed
    *   to face the player.
    *   If the player is not within the spherecast, the turret will
    *   transition to the reset state.
    */

    float timeBetweenShots = 0.25f;
    float attackTimer = 0.0f;

    float activeTime = 5.0f;
    float activeTimer = 0.0f;

    public TurretAttackState(TurretController turret) : base(turret)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Turret is in AttackMode");
    }

    public override void OnStateExit()
    {
        // Play sound?
    }

    public override void OnStateUpdate()
    {
        ShootLaser();
        attackTimer += Time.deltaTime;
    }

    void ShootLaser()
    {
        if (Physics.Raycast(turret.laserOrigin.position, turret.laserOrigin.forward, out RaycastHit hit, turret.laserRange))
        {
            turret.laser.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("Player"))
            {
                activeTimer = 0.0f;
                Debug.Log("Player hit by laser");
                turret.player = hit.transform;
                if (attackTimer >= timeBetweenShots)
                {
                    attackTimer = 0f;
                    Attack();
                }
            }
            else
            {
                CheckPlayerInSight();
            }
        }
        else
        {
            turret.laser.SetPosition(1, turret.laserOrigin.position + (turret.laserOrigin.forward * turret.laserRange));
            CheckPlayerInSight();
        }
    }

    void CheckPlayerInSight()
    {
        // bitwise NOT (~) operator to ignore the layer
        if (Physics.SphereCast(turret.laserOrigin.position, turret.visionSphereRadius, turret.laserOrigin.forward,
            out RaycastHit hit, turret.laserRange - turret.visionSphereRadius, ~turret.ignoreLayer))
        {
            if (hit.transform.CompareTag("Player"))
            {
                activeTimer = 0.0f;
                Debug.Log("Player found");
                turret.player = hit.transform;
                FollowPlayer();
            }
            else
            {
                // Wait for a few seconds to see if the player comes back into sight
                activeTimer += Time.deltaTime;
                if (activeTimer >= activeTime)
                {
                    turret.ChangeState(new TurretResetState(turret));
                }
            }
        }
        else
        {
            // Wait for a few seconds to see if the player comes back into sight
            activeTimer += Time.deltaTime;
            if (activeTimer >= activeTime)
            {
                turret.ChangeState(new TurretResetState(turret));
            }
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = turret.turretBody.transform.position - turret.player.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        turret.turretBody.transform.rotation = Quaternion.Slerp(turret.turretBody.transform.rotation, lookRotation, Time.deltaTime * turret.roationSpeed);
    }

    void Attack()
    {
        if (turret.player != null)
        {
            Health playerHealth = turret.player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.DeductHealth(turret.laserDamagePerSecond * timeBetweenShots);
            }
        }
    }
}
