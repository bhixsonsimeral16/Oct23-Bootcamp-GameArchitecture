using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;
    public Transform[] targetPoints;
    public Transform enemyEye;
    public float playerCheckDistance;
    public float checkRadius = 0.8f;
    public NavMeshAgent agent;

    [HideInInspector] public Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Set a default state
        currentState = new EnemyIdleState(this);
        currentState.OnStateEnter();
    }

    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + transform.forward * playerCheckDistance, checkRadius);
        Gizmos.DrawLine(enemyEye.position, enemyEye.position + transform.forward * playerCheckDistance);
    }
}
