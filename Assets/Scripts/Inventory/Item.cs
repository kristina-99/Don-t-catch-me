using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    public WeaponType weaponType;
    public string displayName;
    public int damageBuff;

    public void PickUp(PlayerInventory inventory)
    {
        inventory.Collect(new WeaponData(displayName, weaponType, damageBuff));
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            return;
        }

        PickUp(inventory);
    }
}
