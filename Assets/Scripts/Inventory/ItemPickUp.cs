using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    [SerializeField] Image image;
    public PlayerManager playerManager;
    
    private const float DestroyObjectDelay = 0.3f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(item.name == "FlameThrower" && !playerManager.HasFlameThrower)
            {
                playerManager.HasFlameThrower = true;
                image.enabled = false;
                Debug.Log("Flamethrower added to the inventory");
                Destroy(this.gameObject, DestroyObjectDelay);
            }
            else if(item.name == "Knife" && !playerManager.HasKnife)
            {
                playerManager.HasKnife = true;
                image.enabled = false;
                Debug.Log("Knife added to the inventory");
                Destroy(this.gameObject, DestroyObjectDelay);
            }
        }
    }
}
