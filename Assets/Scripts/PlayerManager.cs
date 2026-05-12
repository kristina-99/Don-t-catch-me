using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerStats playerStats;
    private bool hasKnife = false;
    private bool hasFlameThrower = false; 
    private const int BasicDamage = 10;

    public bool HasFlameThrower
    {
        get
        {
            return hasFlameThrower;
        }
        set
        {
            hasFlameThrower = value;
        }
    }

    public bool HasKnife
    {
        get
        {
            return hasKnife;
        }
        set
        {
            hasKnife = value;
        }
    }

    public void changeWeapon(bool hasWeapon, int buffDamage, string newWeapon, ref string currentWeapon)
    {
        if(hasWeapon && newWeapon != currentWeapon)
        {
            currentWeapon = newWeapon;
            playerStats.Damage = BasicDamage + buffDamage;
            Debug.Log("Damage is now " + playerStats.Damage );
        }
    }
}
