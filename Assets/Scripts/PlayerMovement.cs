using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Vector2 moveInput;
    private float playerSpeed = 10f;


    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
        Flip();
    }

    void OnMove(InputValue moveInputValue)
    {
        moveInput = moveInputValue.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, myRigidbody2D.linearVelocity.y);
        myRigidbody2D.linearVelocity = playerVelocity;
    }

    void Flip()
    {
        bool isMoving = Mathf.Abs(myRigidbody2D.linearVelocity.x) > Mathf.Epsilon;
        if(isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.linearVelocity.x),1f);
        }
    }
}
