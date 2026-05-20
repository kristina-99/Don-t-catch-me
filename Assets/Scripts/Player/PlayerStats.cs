using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private const int BaseDamage = 10;
    private int healthPoints = 100;
    private int damage = 10;
    
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackText;

    void Start()
    {
        hpText.text = "HP: " + healthPoints;
        attackText.text = "Attack: " + damage;
    }

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
                attackText.text = "Attack: " + damage;
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
            hpText.text = "HP: " + healthPoints;
        }
    }

    public void BuffDamage(int buff)
    {
        Damage = BaseDamage + buff;
    }
}
