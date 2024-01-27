using UnityEngine;

public class EnemyFollowState : EnemyState
{
    float distanceToPlayer;
    float followDistance = 13f;
    float attackDistance = 2f;

    public EnemyFollowState(EnemyController enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy is in Follow");
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if(enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

            if(distanceToPlayer >= followDistance)
            {
                // Player is too far (Idle)
                enemy.ChangeState(new EnemyIdleState(enemy));
                return;
            }

            if(distanceToPlayer < attackDistance)
            {
                // Player is close (Attack)
                enemy.ChangeState(new EnemyAttackState(enemy));
                return;
            }
            
            enemy.agent.SetDestination(enemy.player.position);
        }
        else
        {
            // Player is gone (Idle)
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }
}
