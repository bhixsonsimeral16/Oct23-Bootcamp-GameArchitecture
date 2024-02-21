using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretResetState : TurretState
{
    /*
    *   Turret will rotate back to the initial state.
    *   Turret will then transition to the idle state.
    */
    public TurretResetState(TurretController turret) : base(turret)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Turret is in ResetMode");
        turret.laser.enabled = false;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        ResetTurret();
    }

    void ResetTurret()
    {
        float bodyRotation = turret.turretBody.transform.localEulerAngles.y;
        if (bodyRotation > 0.5f && bodyRotation < 180.0f)
        {
            turret.turretBody.transform.Rotate(Time.deltaTime * turret.roationSpeed * Vector3.down);
        }
        else if (bodyRotation < 359.5f && bodyRotation > 180.0f)
        {
            turret.turretBody.transform.Rotate(Time.deltaTime * turret.roationSpeed * Vector3.up);
        }
        else
        {
            turret.ChangeState(new TurretIdleState(turret));
        }
    }
}
