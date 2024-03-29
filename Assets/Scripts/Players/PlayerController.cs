using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Weapons;

namespace Players
{
    public class PlayerController : NetworkBehaviour
    {
        // input
        Inputs inputs;
        Vector2 moveInput;

        // components
        Rigidbody body;
        
        // member variables
        [SerializeField] private int health = 100;

        // movement
        [SerializeField, Range(1, 10)]
        private float maxVelocity = 5;

        [SerializeField, Range(1, 50)]
        private float acceleration = 10;
        Vector3 desiredVelocity = new(0, 0, 0);
        private float diagonalRegulator = 1;

        // weapons
        private Weapon currentWeapon;

        // Start is called before the first frame update
        void Awake()
        {
            body = GetComponent<Rigidbody>();
            // inputs - IMPORTANT: any time you subscribe an input action to a function, unsubscribe it in OnDestroy.
            // Otherwise, when you reload the scene, the input will still be subscribed the function in the destroyed object,
            // and using the input will cause a NullObjectReference error and subsequently crash the game.
            inputs = new Inputs();
            inputs.Player.Enable();
            inputs.Player.Fire.performed += ShootWeapon;
            
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            Initialize();
        }

        private void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            GetComponent<NetworkObject>().ChangeOwnership(this.NetworkObjectId);
            // transform.position = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
        }

        // Update is called once per frame
        void Update()
        {
            moveInput = inputs.Player.Move.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            if (!IsOwner)
                return;

            // desiredVelocity = new(moveInput.x, 0.0f, moveInput.y);

            if (moveInput.x != 0 && moveInput.y != 0)
                diagonalRegulator = 2;
            else
                diagonalRegulator = 1;
            // normalize input values
            if (moveInput.x < 0)
                desiredVelocity.x =
                    -1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.x;
            else if (moveInput.x > 0)
                desiredVelocity.x =
                    1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.x;
            else
                desiredVelocity.x = 0;

            if (moveInput.y < 0)
                desiredVelocity.z =
                    -1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.z;
            else if (moveInput.y > 0)
                desiredVelocity.z =
                    1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.z;
            else
                desiredVelocity.z = 0;

            // clamping
            desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxVelocity);

            // apply movements to rigidbody
            body.velocity = desiredVelocity;
        }

        void ShootWeapon(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Debug.Log("pew pew");
        }
        
        public void Heal(int amount)
        {
            this.health += amount;
            // plus animation for healing up
        }

        public void TakeDamage(int amount)
        {
            this.health -= amount;
            // plus animation for taking damage
        }

        // void OnDestroy()
        // {
        //     // unsubscribe inputs
        //     inputs.Player.Fire.performed -= shootWeapon;
        // }
    }
}
