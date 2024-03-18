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
        private float speed = 8;

        [SerializeField]
        private float smoothTime = 0.05f;
        private float currentVelocity;

        protected Inputs physicalInputs;

        int rotationTimer = 0;

        public GameObject camObject; // this should be the cam specific to this player
        protected Camera cam;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            physicalInputs = new Inputs();
            physicalInputs.Player.Enable();
            cam = camObject.GetComponent<Camera>();
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
                direction = (
                    GameManager.GetMousePosition3NotNormalized()
                    - GetScreenCoordinatesNotNormalized()
                ).normalized;

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
        protected void AttachWeapon(Weapon weapon, Vector3? weaponOffsetInput = null)
        {
            // Debug.Log("Weapon offset input: " + weaponOffsetInput);
            Vector3 weaponOffset;
            if (weaponOffsetInput == null)
                weaponOffset = new(-0.85f, 0, 0);
            else
                weaponOffset = new(
                    weaponOffsetInput.Value.x,
                    weaponOffsetInput.Value.y,
                    weaponOffsetInput.Value.z
                );

            // Debug.Log("Weapon offset value: " + weaponOffsetInput.Value);

            Debug.Log("Weapon offset: " + weaponOffset);
            currentWeapon = Instantiate(weapon, transform.position, transform.rotation);

            currentWeapon.transform.parent = transform;
            currentWeapon.transform.SetLocalPositionAndRotation(weaponOffset, Quaternion.identity);
            currentWeapon.SetPlayer(this);

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

        public Vector3 GetScreenCoordinates()
        {
            Vector3 weaponPos = cam.WorldToScreenPoint(transform.position);
            // The z corrdinate is the distance from the camera to the player, so we don't care about that.
            // Note that these axes would (most likely) change if we were to rotate the whole world along the X or Z axes (the ones not perpendicular to the ground)
            weaponPos = new(weaponPos.x - (Screen.width / 2), 0, weaponPos.y - (Screen.height / 2));
            return weaponPos.normalized;
        }

        public Vector3 GetScreenCoordinatesNotNormalized()
        {
            Vector3 weaponPos = cam.WorldToScreenPoint(transform.position);
            // The z corrdinate is the distance from the camera to the player, so we don't care about that.
            // Note that these axes would (most likely) change if we were to rotate the whole world along the X or Z axes (the ones not perpendicular to the ground)
            weaponPos = new(weaponPos.x - (Screen.width / 2), 0, weaponPos.y - (Screen.height / 2));
            return weaponPos;
        }
    }
}
