using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerStats playerStats;
    public Animator playerAnimator;
    public Enemy closestEnemy = null;
    private const float attackRange = 10f;
    private const float DelayBeforeDeath = 1.0f;
    private Enemy[] allEnemies;
    private float distanceToClosestEnemy;

    void Start()
    {
        allEnemies = GameObject.FindObjectsByType<Enemy>();
    }

    void Update()
    {
        if(allEnemies.Length > 0)
        {
            FindClosestEnemy();
        }

        if(playerStats.HealthPoints <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        if(attackRange>=distanceToClosestEnemy)
        {
            closestEnemy.enemyStats.HealthPoints -= playerStats.Damage;
            Debug.Log("The player has attacked the enemy and now the enemy has " +  closestEnemy.enemyStats.HealthPoints + " left");
        }
    }

    public void FinishAttack()
    {
        playerAnimator.SetBool("isAttacking", false);
    }

    void FindClosestEnemy()
    {
        allEnemies = GameObject.FindObjectsByType<Enemy>();
        distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy = null;

        foreach(Enemy currentEnemy in allEnemies)
        {
            float distanceToCurrentEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;

            if(distanceToClosestEnemy > distanceToCurrentEnemy)
            {
                distanceToClosestEnemy = distanceToCurrentEnemy;
                closestEnemy = currentEnemy;
            }
        }

    }

    public void Die()
    {
        playerAnimator.SetBool("isDying", true);
    }

    public void FinishDie()
    {
        Destroy(gameObject, DelayBeforeDeath);
    }
}
