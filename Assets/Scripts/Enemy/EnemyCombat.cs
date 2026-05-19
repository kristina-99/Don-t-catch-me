using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public Animator enemyAnimator;
    private float distanceToPlayer;
    private const float AttackRange = 10f;
    private float timeSinceLastAttack = 0f;

    void Start()
    {
        enemyAnimator.SetBool("isWalking", true);
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        float distanceToPlayer = (playerManager.gameObject.transform.position - this.transform.position).sqrMagnitude;
        if(AttackRange >= distanceToPlayer && timeSinceLastAttack > 1.0f)
        {
            Attack();
            Debug.Log("Enemy has attacked the player and the player has " + playerStats.HealthPoints + " healthpoints now");
            timeSinceLastAttack = 0f;
        }
    }

    public void Attack()
    {
        enemyAnimator.SetBool("isWalking", false);
        enemyAnimator.SetBool("isAttacking", true);
        playerStats.HealthPoints -= enemyStats.Damage; 
    }

    public void FinishAttack()
    {
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isWalking", true);
    }
}
