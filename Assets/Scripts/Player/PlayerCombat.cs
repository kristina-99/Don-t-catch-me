using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerStats playerStats;
    public Animator playerAnimator;
    public Enemy closestEnemy;
    private Enemy[] allEnemies;
    private float distanceToClosestEnemy;
    private const float attackRange = 10f;

    void Update()
    {
        FindClosestEnemy();
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
        distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy = null;
        allEnemies = GameObject.FindObjectsByType<Enemy>();

        foreach(Enemy currentEnemy in allEnemies)
        {
            float distanceToCurrentEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;

            if(distanceToClosestEnemy > distanceToCurrentEnemy)
            {
                distanceToClosestEnemy = distanceToCurrentEnemy;
                closestEnemy = currentEnemy;
            }
        }

        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }
}
