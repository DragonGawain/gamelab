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
    [SerializeField, Range(1,10)] float maxVelocity = 2;
    [SerializeField, Range(1,10)] float acceleration = 5f;
    Vector3 desiredVelocity = new(0,0,0);
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
        // normalize input values
        if (moveInput.x < 0)
            desiredVelocity.x = -1 * acceleration + body.velocity.x;
        else if (moveInput.x > 0)
            desiredVelocity.x = 1 * acceleration + body.velocity.x;
        else
            desiredVelocity.x = body.velocity.x;

        if (moveInput.y < 0)
            desiredVelocity.z = -1 * acceleration + body.velocity.z;
        else if (moveInput.y > 0)
            desiredVelocity.z = 1 * acceleration + body.velocity.z;
        else
            desiredVelocity.z = body.velocity.z;

        // clamping
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxVelocity);
        
        // apply movements to rigidbody
        body.velocity = desiredVelocity;


            
    }
}



// UnityEngine.InputSystem.InputAction.CallbackContext ctx