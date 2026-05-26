using System;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public Animator enemyAnimator;

    public event Action OnFinishedDying;

    private const float AttackFrequency = 1.0f;
    private const float DelayBeforeDeath = 1.0f;

    private float timeSinceLastAttack;

    private void Start()
    {
        enemyStats.OnDied += HandleDied;
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void TryAttack()
    {
        if (timeSinceLastAttack < AttackFrequency)
        {
            return;
        }

        if (enemyAnimator.GetBool("isAttacking"))
        {
            return;
        }

        Attack();
    }

    public void FinishAttack()
    {
        enemyAnimator.SetBool("isAttacking", false);
    }

    public void FinishDie()
    {
        OnFinishedDying?.Invoke();
        Destroy(gameObject, DelayBeforeDeath);
    }

    private void Attack()
    {
        enemyAnimator.SetBool("isAttacking", true);
        playerStats.HealthPoints -= enemyStats.Damage;
        timeSinceLastAttack = 0f;
    }

    private void HandleDied()
    {
        enemyAnimator.SetBool("isDying", true);
    }
}
