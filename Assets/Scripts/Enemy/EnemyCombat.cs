using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCombat : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public EnemyMovement enemyMovement;
    public Animator enemyAnimator;
    public Rigidbody2D playerBody;
    public Rigidbody2D enemyBody;
    private const float AttackRange = 10f;
    private const float DelayBeforeDeath = 1.0f;
    private const float AttackFrequency = 1.0f;
    private float distanceToPlayer;
    private float timeSinceLastAttack = 0f;

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        float distanceToPlayer = (playerManager.gameObject.transform.position - this.transform.position).sqrMagnitude;

        if(enemyStats.HealthPoints <= 0)
        {
            Die();
        }

        if(AttackRange >= distanceToPlayer)
        {
            enemyAnimator.SetBool("isWalking", false);  
            if(timeSinceLastAttack > AttackFrequency && !enemyAnimator.GetBool("isAttacking"))
            {
                Attack();
                Debug.Log("Enemy has attacked the player and the player has " + playerStats.HealthPoints + " healthpoints now");
                timeSinceLastAttack = 0f;
            }
        }
        else if(distanceToPlayer > AttackRange)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
    }

    public void Attack()
    {
        if(enemyBody.position.x > playerBody.position.x && enemyBody.transform.localScale.x > 0)
        {
            enemyMovement.Flip();
        }
        else if(playerBody.position.x > enemyBody.position.x && enemyBody.transform.localScale.x < 0)
        {
            enemyMovement.Flip();
        }
        enemyAnimator.SetBool("isAttacking", true);
        playerStats.HealthPoints -= enemyStats.Damage; 
    }

    public void FinishAttack()
    {
       enemyAnimator.SetBool("isAttacking", false);
    }

    public void Die()
    {
        enemyAnimator.SetBool("isDying", true);
    }

    public void FinishDie()
    {
        Destroy(gameObject, DelayBeforeDeath);
        SceneManager.LoadScene("SampleScene");
    }
}