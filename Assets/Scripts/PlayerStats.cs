using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int healthPoints = 100;
    private int damage = 10;

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            if(value > 0 && value < 100)
            {
                damage = value;
            }
        }
    }
}
