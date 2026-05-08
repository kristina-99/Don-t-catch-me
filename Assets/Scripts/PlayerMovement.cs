using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D;
    public BoxCollider2D feetCollider;
    public CapsuleCollider2D bodyCollider;
    private Vector2 moveInput;
    private Animator playerAnimator;
    private float playerSpeed = 10f;
    private float jumpSpeed = 10f;
    private bool isMoving;

    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

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

    void OnJump(InputValue jumpInputValue)
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidbody2D.linearVelocity += new Vector2(0f,jumpSpeed);
        }    
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, playerRigidbody2D.linearVelocity.y);
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
