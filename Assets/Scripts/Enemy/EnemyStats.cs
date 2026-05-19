using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private int healthPoints = 100;
    private int damage = 10;
    
    public int Damage
    {
        get
        {
            return damage;
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
}
