using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Weapons;

namespace Players
{
    abstract public class PlayerTestScript : MonoBehaviour
    {
        private Vector2 input;
        private CharacterController characterController;
        private Vector3 direction;
        protected Weapon currentWeapon;

        [SerializeField]
        private float speed = 5;

        [SerializeField]
        private float smoothTime = 0.05f;
        private float currentVelocity;

        protected Inputs physicalInputs;

        int rotationTimer = 0;
  

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            physicalInputs = new Inputs();
            physicalInputs.Player.Enable();
            OnAwake();
            //AttachWeapon(flamethrower); //giving player a flamethrower to test
        }

        private void Update() { }

        private void FixedUpdate()
        {
            // if (input.sqrMagnitude == 0)
            //     return;
            direction = GetMoveInput();
            // only move if there's a move input
            if (direction != Vector3.zero)
            {
                characterController.Move(speed * Time.deltaTime * direction);
            }


            // if you have fired within the past 5 seconds, rotate to look in the direction of fire
            if (rotationTimer > 0)
                direction = GameManager.GetMousePosition3(); 
            
            // if you have fired recently or you have put in a move input, rotate
            if (rotationTimer > 0 || direction != Vector3.zero)
            {
                var targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg) + 180;
                var angle = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    targetAngle,
                    ref currentVelocity,
                    smoothTime
                );
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            }
            
            if (rotationTimer >= 0)
                rotationTimer--;
        }

        // attaches the gun object to the character and stores it in "currentWeapon"
        protected void AttachWeapon(Weapon weapon, float offsetZ = 0.85f)
        {
            currentWeapon = Instantiate(weapon, transform.position, transform.rotation);
            
            currentWeapon.transform.parent = transform;
            currentWeapon.transform.SetLocalPositionAndRotation(new Vector3(-offsetZ,0,0), new Quaternion(0,0,0,0));
            
            Debug.Log(this.transform.forward);
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            // Simple movement for testing purposes
            input = ctx.ReadValue<Vector2>();
            direction = new Vector3(input.x, 0.0f, input.y);
        }

        // Called when the player fires with LMB or RT on controller
        virtual public void Fire(InputAction.CallbackContext ctx)
        {
            if (!currentWeapon)
                return;
            rotationTimer = 250; // 50 FU's/sec -> 250/50 = 5 seconds
        }

        // Called when the player swaps weapons with Q or RB on controller
        virtual public void SwapWeapon(InputAction.CallbackContext ctx)
        {
            // despawn current weapon
            // add other weapon
            // update currentWeapon
            // reflect that on the UI

            if (!currentWeapon)
                return;
        }

        abstract protected void OnAwake();
        abstract protected Vector3 GetMoveInput();
    }
}
