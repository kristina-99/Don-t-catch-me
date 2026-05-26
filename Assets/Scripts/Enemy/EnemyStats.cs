using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    public event Action OnDied;

    private const int BaseHealthPoints = 100;
    private const int BaseDamage = 10;

    private int healthPoints;
    private int damage;
    private bool isDead;

    private void Awake()
    {
        healthPoints = BaseHealthPoints;
        damage = BaseDamage;
    }

    public int MaxHealthPoints
    {
        get { return BaseHealthPoints; }
    }

    public int Damage
    {
        get { return damage; }
    }

    public int HealthPoints
    {
        get { return healthPoints; }
        set
        {
            if (isDead)
            {
                return;
            }

            healthPoints = value;
            OnHealthChanged?.Invoke(healthPoints);

            if (healthPoints <= 0)
            {
                isDead = true;
                OnDied?.Invoke();
            }
        }
    }
}
