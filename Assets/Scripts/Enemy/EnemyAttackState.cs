using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float distanceToPlayer;
    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy is in Attack");
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

            // TODO: Attack player
            if(distanceToPlayer > 2f)
            {
                // Player is too far (Follow)
                enemy.ChangeState(new EnemyFollowState(enemy));
            }
        }
        else
        {
            // Player is gone (Idle)
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }
}
