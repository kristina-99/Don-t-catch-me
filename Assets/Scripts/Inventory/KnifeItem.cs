using UnityEngine;
using UnityEngine.UI;

public class KnifeItem : MonoBehaviour,IItem
{
    public PlayerManager playerManager;
    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public Image lockKnife;
    public int damageBuff =>15;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp();
        }
        Destroy(this.gameObject);
        lockKnife.enabled = false;
    }

    public void PickUp()
    {
        if(!playerManager.HasKnife)
        {
            playerManager.HasKnife = true;
        }
    }

    public void Equip()
    {
        playerMovement.currentWeapon = "Knife";
        playerStats.buffDamage(damageBuff);   
    }

}
