using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int healthPoints = 100;
    private int damage = 10;

    private const int BaseDamage = 10;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            if(value > 0 && value <= 100)
            {
                damage = value;
            }
        }
    }

    public int HealthPoints
    {
        get
        {
            return healthPoints;
        }
        set
        {
            healthPoints = value;
        }
    }

    public void buffDamage(int buff)
    {
        Damage = BaseDamage + buff;
    }
}
