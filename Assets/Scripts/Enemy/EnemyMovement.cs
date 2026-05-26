using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public Animator enemyAnimator;
    public BoxCollider2D feetCollider;

    public event Action OnWallHit;
    public float FacingDirection => Mathf.Sign(transform.localScale.x);

    private const float PatrolSpeed = 1f;
    private const float ChaseSpeed = 2f;
    private const float JumpClearance = 0.4f;
    private const float TileSize = 1f;
    private const float LookAheadDistance = 2f;
    private const int MaxJumpableTiles = 2;

    public void Patrol()
    {
        enemyAnimator.SetBool("isWalking", true);
        TryJumpAhead();
        rigidBody2D.linearVelocity = new Vector2(FacingDirection * PatrolSpeed, GetYVelocity());
    }

    public void MoveTo(Vector2 targetPosition)
    {
        enemyAnimator.SetBool("isWalking", true);
        float direction = Mathf.Sign(targetPosition.x - transform.position.x);
        transform.localScale = new Vector2(direction, 1f);
        TryJumpAhead();
        rigidBody2D.linearVelocity = new Vector2(direction * ChaseSpeed, GetYVelocity());
    }

    public void Stop()
    {
        enemyAnimator.SetBool("isWalking", false);
        rigidBody2D.linearVelocity = new Vector2(0f, GetYVelocity());
    }

    public void FlipDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsWallContact(collision))
        {
            return;
        }

        if (Mathf.Abs(rigidBody2D.linearVelocity.x) > Mathf.Epsilon && IsGrounded())
        {
            OnWallHit?.Invoke();
        }
    }

    private bool IsWallContact(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return false;
        }

        if (collision.contactCount == 0)
        {
            return false;
        }

        return Mathf.Abs(collision.contacts[0].normal.x) > 0.5f;
    }

    private void TryJumpAhead()
    {
        if (!IsGrounded() || rigidBody2D.linearVelocity.y > 0f)
        {
            return;
        }

        float jumpHeight = DetectRequiredJumpHeight();
        if (jumpHeight > 0f)
        {
            Jump(jumpHeight);
        }
    }

    private float DetectRequiredJumpHeight()
    {
        float moveDir = Mathf.Sign(rigidBody2D.linearVelocity.x);
        float feetY = feetCollider.bounds.min.y;
        LayerMask groundMask = LayerMask.GetMask("Ground");
        Vector2 forward = Vector2.right * moveDir;

        bool wallAhead = Physics2D.Raycast(
            new Vector2(transform.position.x, feetY + 0.1f),
            forward, LookAheadDistance, groundMask);

        if (!wallAhead)
        {
            return 0f;
        }

        for (int tile = 1; tile <= MaxJumpableTiles; tile++)
        {
            bool blockedAtHeight = Physics2D.Raycast(
                new Vector2(transform.position.x, feetY + tile * TileSize + 0.1f),
                forward, LookAheadDistance, groundMask);

            if (!blockedAtHeight)
            {
                return tile * TileSize + JumpClearance;
            }
        }

        return 0f;
    }

    private void Jump(float obstacleHeight)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y * rigidBody2D.gravityScale);
        float jumpVelocity = Mathf.Sqrt(2f * gravity * (obstacleHeight + JumpClearance));
        rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, jumpVelocity);
    }

    private bool IsGrounded()
    {
        if (feetCollider == null)
        {
            return false;
        }

        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private float GetYVelocity()
    {
        return IsGrounded() && rigidBody2D.linearVelocity.y <= 0f ? 0f : rigidBody2D.linearVelocity.y;
    }
}
