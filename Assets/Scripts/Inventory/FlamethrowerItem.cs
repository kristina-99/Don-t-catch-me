using UnityEngine;
using UnityEngine.UI;

public class FlamethrowerItem : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public Image lockFlamethrower;
    public int damageBuff =>40;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp();
        }
        Destroy(this.gameObject);
        lockFlamethrower.enabled = false;
    }

    public void PickUp()
    {
        if(!playerManager.HasFlameThrower)
        {
            playerManager.HasFlameThrower = true;
        }
    }

    public void Equip()
    {
        playerMovement.currentWeapon = "Flamethrower";
        playerStats.buffDamage(damageBuff);
    }

}
