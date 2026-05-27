using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody2D;
    public BoxCollider2D feetCollider;
    public Animator playerAnimator;

    private const float PlayerSpeed = 10f;
    private const float JumpSpeed = 10f;

    private Vector2 moveInput;

    private void Update()
    {
        Run();
        Flip();
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void Jump()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidbody2D.linearVelocity += new Vector2(0f, JumpSpeed);
        }
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * PlayerSpeed, playerRigidbody2D.linearVelocity.y);
        playerRigidbody2D.linearVelocity = playerVelocity;
        playerAnimator.SetBool("isRunning", Mathf.Abs(playerRigidbody2D.linearVelocity.x) > Mathf.Epsilon);
    }

    private void Flip()
    {
        bool hasHorizontalSpeed = Mathf.Abs(playerRigidbody2D.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody2D.linearVelocity.x), 1f);
        }
    }
}
