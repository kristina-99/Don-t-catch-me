using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Vector2 moveInput;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void OnMove(InputValue moveInputValue)
    {
        moveInput = moveInputValue.Get<Vector2>();
    }
}
