using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyState
{
    int currentTargetPoint = 0;

    public EnemyIdleState(EnemyController enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        enemy.agent.SetDestination(enemy.targetPoints[currentTargetPoint].position);
        Debug.Log("Enemy is Idle");
    }

    public override void OnStateExit()
    {
        // TODO
    }

    public override void OnStateUpdate()
    {
        if(enemy.agent.remainingDistance <= 0.1f)
        {
            currentTargetPoint++;
            if(currentTargetPoint >= enemy.targetPoints.Length)
            {
                currentTargetPoint = 0;
            }
            enemy.agent.SetDestination(enemy.targetPoints[currentTargetPoint].position);
        }

        // Check for player
        if(Physics.SphereCast(enemy.enemyEye.position, enemy.checkRadius, enemy.transform.forward, out RaycastHit hit, enemy.playerCheckDistance))
        {
            Debug.DrawRay(enemy.enemyEye.position, enemy.transform.forward * hit.distance, Color.yellow);
            if(hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found");
                enemy.player = hit.transform;
                enemy.agent.SetDestination(enemy.player.position);

                // Move to follow state
                enemy.ChangeState(new EnemyFollowState(enemy));
            }
        }
    }
}
