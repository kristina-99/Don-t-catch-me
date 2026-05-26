using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public Animator enemyAnimator;
    public BoxCollider2D feetCollider;

    private const float PatrolSpeed = 1f;
    private const float ChaseSpeed = 2f;
    private const float JumpSpeed = 8f;

    public void Patrol()
    {
        enemyAnimator.SetBool("isWalking", true);
        rigidBody2D.linearVelocity = new Vector2(Mathf.Sign(transform.localScale.x) * PatrolSpeed, GetYVelocity());
    }

    public void MoveTo(Vector2 targetPosition)
    {
        enemyAnimator.SetBool("isWalking", true);
        float direction = Mathf.Sign(targetPosition.x - transform.position.x);
        transform.localScale = new Vector2(direction, 1f);
        rigidBody2D.linearVelocity = new Vector2(direction * ChaseSpeed, GetYVelocity());
    }

    public void Stop()
    {
        enemyAnimator.SetBool("isWalking", false);
        rigidBody2D.linearVelocity = new Vector2(0f, GetYVelocity());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int playerLayerIndex = LayerMask.NameToLayer("Player");
        bool hasHorizontalSpeed = Mathf.Abs(rigidBody2D.linearVelocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed && collision.gameObject.layer != playerLayerIndex)
        {
            if (IsGrounded())
            {
                Jump();
            }
            else
            {
                FlipPatrolDirection();
            }
        }
    }

    private void Jump()
    {
        rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, JumpSpeed);
    }

    private bool IsGrounded()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private float GetYVelocity()
    {
        return IsGrounded() ? 0f : rigidBody2D.linearVelocity.y;
    }

    private void FlipPatrolDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }
}
