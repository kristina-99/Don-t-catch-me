using UnityEngine;

public interface IItem
{
    int damageBuff{ get; }

    void OnTriggerEnter2D(Collider2D collision);

    void PickUp();
    
    void Equip();
}
