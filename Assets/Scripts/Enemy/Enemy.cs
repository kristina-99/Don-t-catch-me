using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats enemyStats;

    public void TakeDamage(int damage)
    {
        enemyStats.HealthPoints -= damage;
    }
}
