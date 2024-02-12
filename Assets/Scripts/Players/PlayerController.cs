using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // input
    Inputs inputs;
    Vector2 moveInput;

    // components
    Rigidbody body;

    // movement
    [SerializeField, Range(1,10)] float maxVelocity = 5;
    [SerializeField, Range(1,50)] float acceleration = 10;
    Vector3 desiredVelocity = new(0,0,0);
    float diagonalRegulator = 1;
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        inputs = new Inputs();
        inputs.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = inputs.Player.Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (moveInput.x != 0 && moveInput.y != 0)
            diagonalRegulator = 2;
        else
            diagonalRegulator = 1;
        // normalize input values
        if (moveInput.x < 0)
            desiredVelocity.x = -1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.x;
        else if (moveInput.x > 0)
            desiredVelocity.x = 1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.x;
        else
            desiredVelocity.x = 0;

        if (moveInput.y < 0)
            desiredVelocity.z = -1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.z;
        else if (moveInput.y > 0)
            desiredVelocity.z = 1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.z;
        else
            desiredVelocity.z = 0;

        // clamping
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxVelocity);
        
        // apply movements to rigidbody
        body.velocity = desiredVelocity;


            
    }
}



// UnityEngine.InputSystem.InputAction.CallbackContext ctx