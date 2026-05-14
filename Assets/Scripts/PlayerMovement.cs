using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerStats playerStats;
    public ItemManager itemManager;
    public KnifeItem knifeItem;
    public FlamethrowerItem flamethrowerItem;
    public BoxCollider2D feetCollider;
    public CapsuleCollider2D bodyCollider;
    public Rigidbody2D playerRigidbody2D;
    public Animator playerAnimator;
    public Image tickKnife;
    public Image tickSword;
    public Image tickFlamethrower;
    public string currentWeapon = "Sword";
    private Vector2 moveInput;
    private bool isMoving;
    private const float PlayerSpeed = 10f;
    private const float JumpSpeed = 10f;
    private const int BasicDamage = 10;

    void Update()
    {
        isMoving = Mathf.Abs(playerRigidbody2D.linearVelocity.x) > Mathf.Epsilon;
        Run();
        Flip();
    }

    void OnMove(InputValue moveInputValue)
    {
        moveInput = moveInputValue.Get<Vector2>();
    }

    void OnJump()
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidbody2D.linearVelocity += new Vector2(0f,JumpSpeed);
        }    
    }

    void OnUseItemSword()
    {
        itemManager.DisableItemImages();
        tickSword.enabled = true;
        if(currentWeapon != "Sword")
        {
            currentWeapon = "Sword";
            playerStats.Damage = BasicDamage;
            Debug.Log("Damage is:" + playerStats.Damage);
        }
    }

    void OnUseItemFlamethrower()
    {
        itemManager.DisableItemImages();
        tickFlamethrower.enabled = true;
        if(playerManager.HasFlameThrower && currentWeapon != "Flamethrower")
        {
            flamethrowerItem.Equip();
            Debug.Log("Damage is:" + playerStats.Damage);
        }
    }

    void OnUseItemKnife()
    {
        itemManager.DisableItemImages();
        tickKnife.enabled = true;
        if(playerManager.HasKnife && currentWeapon != "Knife")
        {
            knifeItem.Equip();
            Debug.Log("Damage is:" + playerStats.Damage);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * PlayerSpeed, playerRigidbody2D.linearVelocity.y);
        playerRigidbody2D.linearVelocity = playerVelocity;

        playerAnimator.SetBool("isRunning",isMoving);
    }

    void Flip()
    {
        if(isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody2D.linearVelocity.x),1f);
        }
    }

}
