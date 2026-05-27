using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerInventory playerInventory;
    public Animator playerAnimator;
    public ParticleSystem flamethrowerParticles;
    public Enemy[] cachedEnemies;

    private const float AttackRangeSquared = 100f;

    private void Start()
    {
        playerStats.OnDied += HandleDied;
    }

    public void Attack()
    {
        playerAnimator.SetBool("isAttacking", true);
        PerformAttack();
    }

    public void FinishAttack()
    {
        playerAnimator.SetBool("isAttacking", false);
    }

    public void FinishDie()
    {
        Destroy(gameObject);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        cachedEnemies = Array.FindAll(cachedEnemies, cachedEnemy => cachedEnemy != enemy);
    }

    private void PerformAttack()
    {
        if (playerInventory.EquippedWeapon.Type == WeaponType.Flamethrower && flamethrowerParticles != null)
        {
            flamethrowerParticles.Play();
        }

        Enemy closestEnemy = FindClosestEnemy();
        if (closestEnemy == null)
        {
            return;
        }

        float distanceSquared = (closestEnemy.transform.position - transform.position).sqrMagnitude;
        if (distanceSquared <= AttackRangeSquared)
        {
            closestEnemy.TakeDamage(playerStats.Damage);
        }
    }

    private void HandleDied()
    {
        playerAnimator.SetBool("isDying", true);
    }

    private Enemy FindClosestEnemy()
    {
        Enemy closest = null;
        float closestDistanceSquared = Mathf.Infinity;

        foreach (Enemy enemy in cachedEnemies)
        {
            if (enemy == null)
            {
                continue;
            }

            float distanceSquared = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distanceSquared < closestDistanceSquared)
            {
                closestDistanceSquared = distanceSquared;
                closest = enemy;
            }
        }

        return closest;
    }
}
