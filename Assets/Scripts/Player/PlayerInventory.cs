using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event Action<WeaponData> OnWeaponChanged;
    public event Action<WeaponType> OnWeaponCollected;

    private Dictionary<WeaponType, WeaponData> collectedWeapons;
    private WeaponData equippedWeapon;

    private void Awake()
    {
        collectedWeapons = new Dictionary<WeaponType, WeaponData>();
        collectedWeapons[WeaponType.Sword] = WeaponData.Sword;
        equippedWeapon = WeaponData.Sword;
    }

    public WeaponData EquippedWeapon
    {
        get { return equippedWeapon; }
    }

    public void Collect(WeaponData weapon)
    {
        collectedWeapons[weapon.Type] = weapon;
        OnWeaponCollected?.Invoke(weapon.Type);
        Equip(weapon);
    }

    public bool TryEquip(WeaponType type)
    {
        WeaponData weapon;
        if (!collectedWeapons.TryGetValue(type, out weapon))
        {
            return false;
        }

        Equip(weapon);
        return true;
    }

    public bool HasWeapon(WeaponType type)
    {
        return collectedWeapons.ContainsKey(type);
    }

    private void Equip(WeaponData weapon)
    {
        equippedWeapon = weapon;
        OnWeaponChanged?.Invoke(equippedWeapon);
    }
}
