using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerCombat combat;
    public PlayerInventory inventory;

    private void OnMove(InputValue value)
    {
        movement.SetMoveInput(value.Get<Vector2>());
    }

    private void OnJump()
    {
        movement.Jump();
    }

    private void OnAttack()
    {
        combat.Attack();
    }

    private void OnUseItemSword()
    {
        inventory.TryEquip(WeaponType.Sword);
    }

    private void OnUseItemKnife()
    {
        inventory.TryEquip(WeaponType.Knife);
    }

    private void OnUseItemFlamethrower()
    {
        inventory.TryEquip(WeaponType.Flamethrower);
    }
}
