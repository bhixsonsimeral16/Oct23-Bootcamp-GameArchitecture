using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] public LineRenderer laser;
    [SerializeField] public Transform laserOrigin;
    [SerializeField] public GameObject turretBody;
    [SerializeField] public LayerMask ignoreLayer;

    [Header("Turret Stats")]
    [SerializeField] public float roationSpeed = 10.0f;
    [Range(0, 90)] public float rotationDegree = 45.0f; // +/- 45 degrees from the initial rotation
    [SerializeField] public float laserRange = 10.0f;
    [SerializeField] public float laserDamagePerSecond = 10.0f;
    [SerializeField] public float visionSphereRadius = 1f;

    TurretState currentState;

    [HideInInspector] public Transform player;

    void Start()
    {
        currentState = new TurretIdleState(this);
        currentState.OnStateEnter();
    }

    void Update()
    {
        laser.SetPosition(0, laser.transform.position);
        currentState.OnStateUpdate();
    }

    public void ChangeState(TurretState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(laserOrigin.position, laserOrigin.forward * laserRange);
        Gizmos.DrawWireSphere(laserOrigin.position + laserOrigin.forward * (laserRange - visionSphereRadius), visionSphereRadius);
    }
}
