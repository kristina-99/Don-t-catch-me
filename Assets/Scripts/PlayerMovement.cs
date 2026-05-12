using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerStats playerStats;
    public BoxCollider2D feetCollider;
    public CapsuleCollider2D bodyCollider;
    public Rigidbody2D playerRigidbody2D;
    public Animator playerAnimator;
    private string currentWeapon = "Sword";
    private Vector2 moveInput;
    private bool isMoving;
    private const float PlayerSpeed = 10f;
    private const float JumpSpeed = 10f;

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

    void OnSelectInventoryItem()
    {
        if(Input.GetKey("1"))
        {
            currentWeapon = "Sword";
            playerStats.Damage = 10;
        }
        else if(Input.GetKey("2"))
        {
             if(playerManager.HasFlameThrower)
             {
                 currentWeapon = "Flamethrower";
                 playerStats.Damage += 15;
             }
        }
        else if(Input.GetKey("3"))
        {
             if(playerManager.HasKnife)
             {
                 currentWeapon = "Knife";
                 playerStats.Damage += 40;
             }
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
