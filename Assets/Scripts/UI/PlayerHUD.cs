using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackText;
    public Image swordSlotIndicator;
    public Image knifeSlotIndicator;
    public Image flamethrowerSlotIndicator;
    public Image knifeLockImage;
    public Image flamethrowerLockImage;
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager.stats.OnHealthChanged += UpdateHealthDisplay;
        playerManager.stats.OnDamageChanged += UpdateDamageDisplay;
        playerManager.inventory.OnWeaponChanged += UpdateWeaponSlotDisplay;
        playerManager.inventory.OnWeaponCollected += UpdateWeaponLockDisplay;

        UpdateHealthDisplay(playerManager.stats.HealthPoints);
        UpdateDamageDisplay(playerManager.stats.Damage);
        UpdateWeaponSlotDisplay(playerManager.inventory.EquippedWeapon);
    }

    private void UpdateHealthDisplay(int healthPoints)
    {
        hpText.text = "HP: " + healthPoints;
    }

    private void UpdateDamageDisplay(int damage)
    {
        attackText.text = "Attack: " + damage;
    }

    private void UpdateWeaponSlotDisplay(WeaponData weapon)
    {
        swordSlotIndicator.enabled = weapon.Type == WeaponType.Sword;
        knifeSlotIndicator.enabled = weapon.Type == WeaponType.Knife;
        flamethrowerSlotIndicator.enabled = weapon.Type == WeaponType.Flamethrower;
    }

    private void UpdateWeaponLockDisplay(WeaponType unlockedType)
    {
        if (unlockedType == WeaponType.Knife)
        {
            knifeLockImage.enabled = false;
        }
        else if (unlockedType == WeaponType.Flamethrower)
        {
            flamethrowerLockImage.enabled = false;
        }
    }
}
