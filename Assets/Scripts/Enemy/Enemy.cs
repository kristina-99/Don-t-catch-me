using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats enemyStats;
    void Start()
    {
        enemyStats = enemyStats.GetComponent<EnemyStats>();
    }
}
