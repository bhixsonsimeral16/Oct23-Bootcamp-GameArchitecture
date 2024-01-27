using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] targetPoints;
    [SerializeField] Transform enemyEye;
    [SerializeField] float playerCheckDistance;
    [SerializeField] float checkRadius = 0.8f;

    private int currentTargetPoint = 0;
    private NavMeshAgent agent;
    
    public bool isIdle = true;
    public bool isPlayerFound, isCloseToPlayer;
    public Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPoints[currentTargetPoint].position);
    }

    void Update()
    {
        if(isIdle)
        {
            Idle();
        }
        else if(isPlayerFound)
        {
            if(isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    // Idle is a patrol pattern
    void Idle()
    {
        if(agent.remainingDistance <= 0.1f)
        {
            currentTargetPoint++;
            if(currentTargetPoint >= targetPoints.Length)
            {
                currentTargetPoint = 0;
            }
            agent.SetDestination(targetPoints[currentTargetPoint].position);
        }

        // Check for player
        if(Physics.SphereCast(enemyEye.position, checkRadius, transform.forward, out RaycastHit hit, playerCheckDistance))
        {
            if(hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found");
                isIdle = false;
                isPlayerFound = true;
                player = hit.transform;
                agent.SetDestination(player.position);
            }
        }
    }

    void FollowPlayer()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.position) >= 10f)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            // Check if player is close (Attack)
            if(Vector3.Distance(transform.position, player.position) < 2f)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }
            
            agent.SetDestination(player.position);
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking the player");
        if(Vector3.Distance(transform.position, player.position) > 2f)
        {
            isCloseToPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + transform.forward * playerCheckDistance, checkRadius);
        Gizmos.DrawLine(enemyEye.position, enemyEye.position + transform.forward * playerCheckDistance);
    }
}
