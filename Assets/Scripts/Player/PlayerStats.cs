using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnDamageChanged;
    public event Action OnDied;

    private const int BaseHealthPoints = 100;
    private const int BaseDamage = 10;
    private const int MinDamage = 1;
    private const int MaxDamage = 100;

    private int healthPoints;
    private int damage;
    private bool isDead;

    private void Awake()
    {
        healthPoints = BaseHealthPoints;
        damage = BaseDamage;
    }

    private void Start()
    {
        playerInventory.OnWeaponChanged += ApplyWeaponDamageBuff;
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

    public int Damage
    {
        get { return damage; }
        set
        {
            if (value >= MinDamage && value <= MaxDamage)
            {
                damage = value;
                OnDamageChanged?.Invoke(damage);
            }
        }
    }

    public void BuffDamage(int buff)
    {
        Damage = BaseDamage + buff;
    }

    private void ApplyWeaponDamageBuff(WeaponData weapon)
    {
        BuffDamage(weapon.DamageBuff);
    }
}
