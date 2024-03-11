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
            if (input.sqrMagnitude == 0)
                return;
            direction = GetMoveInput();
            if (direction == Vector3.zero)
                return;

            var targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg) + 180;
            var angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref currentVelocity,
                smoothTime
            );
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            characterController.Move(speed * Time.deltaTime * direction);
        }

        // attaches the gun object to the character and stores it in "currentWeapon"
        protected void AttachWeapon(Weapon weapon, float offsetZ = 0.834f)
        {
            currentWeapon = Instantiate(weapon, transform.position, transform.rotation);
            currentWeapon.transform.parent = transform;
            currentWeapon.transform.position = new Vector3(
                currentWeapon.transform.position.x,
                currentWeapon.transform.position.y,
                currentWeapon.transform.position.z - offsetZ
            );
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
