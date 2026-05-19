using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public CapsuleCollider2D capsuleCollider2D;
    public Animator enemyAnimator;
    private float enemySpeed = 1f;
    void Update()
    {
        if(enemyAnimator.GetBool("isWalking"))
        {
            rigidBody2D.linearVelocity = new Vector2(enemySpeed, 0f);
        }
        else
        {
            rigidBody2D.linearVelocity = new Vector2(0f, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int playerLayerIndex = LayerMask.NameToLayer("Player");
        bool hasHorizontalSpeed = Mathf.Abs(rigidBody2D.linearVelocity.x) > Mathf.Epsilon;
    
        if(hasHorizontalSpeed  && collision.gameObject.layer != playerLayerIndex)
        {
            enemySpeed = -enemySpeed;
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector2(Mathf.Sign(enemySpeed),1f);
    }
    
}