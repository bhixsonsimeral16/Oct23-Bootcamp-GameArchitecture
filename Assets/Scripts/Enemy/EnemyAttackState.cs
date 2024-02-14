using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float distanceToPlayer;
    Health playerHealth;
    float damagePerSecond = 20f;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        playerHealth = enemy.player.GetComponent<Health>();

        if(playerHealth == null)
        {
            Debug.LogError("Player does not have a Health component");
        }
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

            if(distanceToPlayer > 3f)
            {
                // Player is too far (Follow)
                enemy.ChangeState(new EnemyFollowState(enemy));
            }
            else
            {
                // Player is close enough to attack
                Attack();
            }
        }
        else
        {
            // Player is gone (Idle)
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }

    void Attack()
    {
        if(playerHealth != null)
        {
            playerHealth.DeductHealth(damagePerSecond * Time.deltaTime);
        }
    }
}
