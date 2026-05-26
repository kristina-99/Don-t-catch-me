using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType weaponType;
    public string displayName;
    public int damageBuff;

    private WeaponData weaponData;

    private void Awake()
    {
        weaponData = new WeaponData(displayName, weaponType, damageBuff);
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

        inventory.Collect(weaponData);
        Destroy(gameObject);
    }
}
