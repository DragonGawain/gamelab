using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Weapons;

namespace Players
{
    public class PlayerTestScript : MonoBehaviour
    {

        private Vector2 input;
        private CharacterController characterController;
        private Vector3 direction;
        private Weapon currentWeapon;

        public Flamethrower flamethrower;

        [SerializeField] private float speed = 5;

        [SerializeField] private float smoothTime = 0.05f;
        private float currentVelocity;
        
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            AttachWeapon(flamethrower, 0.834f); //giving player a flamethrower to test 
        }

        private void Update()
        {
            if (input.sqrMagnitude == 0) return;
           
            var targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg) + 180;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            characterController.Move(direction * speed * Time.deltaTime);
        }

        // attaches the gun object to the character and stores it in "currentWeapon"
        private void AttachWeapon(Weapon weapon, float offsetZ)
        {
            currentWeapon = Instantiate(weapon, transform.position, transform.rotation);
            currentWeapon.transform.parent = transform;
            currentWeapon.transform.position = new Vector3(currentWeapon.transform.position.x, 
                currentWeapon.transform.position.y, currentWeapon.transform.position.z - offsetZ);
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            // Simple movement for testing purposes
            input = ctx.ReadValue<Vector2>();
            direction = new Vector3(input.x, 0.0f, input.y);
        }

        public void Fire(InputAction.CallbackContext ctx)
        {
            
            if (!currentWeapon) return;
            
            if (currentWeapon as Flamethrower)
            {
                // Player holds to fire
                if (ctx.performed)
                {
                    (currentWeapon as Flamethrower).Fire();
                }
                // Player releases
                else if (ctx.canceled)
                {
                    (currentWeapon as Flamethrower).StopFire();
                }

            }
            
        }
    }
}