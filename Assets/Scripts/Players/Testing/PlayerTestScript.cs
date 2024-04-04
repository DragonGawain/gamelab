// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.TextCore.Text;
// using UnityEngine.UIElements;
// using Weapons;
// using DG.Tweening;
// using UnityEditor.Animations;
// using Sequence = DG.Tweening.Sequence;
// using System;
// using Unity.Netcode;


// namespace Players
// {
//     abstract public class PlayerTestScript : NetworkBehaviour
//     {
//         private Vector2 input;
//         private CharacterController characterController;
//         private Vector3 direction;
//         protected Animator animator;
//         protected Weapon currentWeapon;
//         public event Action<float> OnHealthChanged; //changing health bar

//         [SerializeField] private int health;
//         //How many seconds until player can get damaged again
//         [SerializeField] private float invincibleSeconds;
//         private float hitTime = 0;
        
        
//         [SerializeField]
//         private float speed = 8;

//         [SerializeField]
//         private float smoothTime = 0.05f;
//         private float currentVelocity;

//         protected Inputs physicalInputs;

//         int rotationTimer = 0;

//         public GameObject camObject; // this should be the cam specific to this player
//         protected Camera cam;

//         //Change player color to see it getting damaged
//         private Renderer _renderer;
//         private Material material;
//         private Color originalColor;
//         private void Awake()
//         {
//             characterController = GetComponent<CharacterController>();
//             physicalInputs = new Inputs();
//             physicalInputs.Player.Enable();
//             cam = camObject.GetComponent<Camera>();
//             OnAwake();
//             //AttachWeapon(flamethrower); //giving player a flamethrower to test
            
//             //For Changing Color when hit 
//             _renderer = GetComponent<Renderer>();
//             material = Instantiate(_renderer.material);
//             _renderer.material = material;
//             originalColor = material.color;

//             animator = GetComponent<Animator>();
//         }

//         private void FixedUpdate()
//         {
//             // if (input.sqrMagnitude == 0)
//             //     return;
//             direction = GetMoveInput();
//             // only move if there's a move input
//             if (direction != Vector3.zero)
//             {
//                 characterController.Move(speed * Time.deltaTime * direction);
//                 if (animator != null)
//                 {
//                     if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
//                         animator.SetTrigger("OnRun");    
//                 }
                
//             }
//             else
//             {
//                 if (animator != null)
//                 {
//                     if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
//                         animator.SetTrigger("OnIdle");    
//                 }
//             }

//             // if you have fired within the past 5 seconds, rotate to look in the direction of fire
//             if (rotationTimer > 0)
//                 direction = -(
//                     GameManager.GetMousePosition3NotNormalized()
//                     - GetScreenCoordinatesNotNormalized()
//                 ).normalized;

//             // if you have fired recently or you have put in a move input, rotate
//             if (rotationTimer > 0 || direction != Vector3.zero)
//             {
//                 var targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg);
//                 var angle = Mathf.SmoothDampAngle(
//                     transform.eulerAngles.y,
//                     targetAngle,
//                     ref currentVelocity,
//                     smoothTime
//                 );
//                 transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
//             }

//             if (rotationTimer >= 0)
//                 rotationTimer--;
//         }

//         // attaches the gun object to the character and stores it in "currentWeapon"
//         protected void AttachWeapon(Weapon weapon, Vector3? weaponOffsetInput = null)
//         {
//             // Debug.Log("Weapon offset input: " + weaponOffsetInput);
//             Vector3 weaponOffset;
//             if (weaponOffsetInput == null)
//                 weaponOffset = new(-0.85f, 0, 0);
//             else
//                 weaponOffset = new(
//                     weaponOffsetInput.Value.x,
//                     weaponOffsetInput.Value.y,
//                     weaponOffsetInput.Value.z
//                 );

//             // Debug.Log("Weapon offset value: " + weaponOffsetInput.Value);

//             //Debug.Log("Weapon offset: " + weaponOffset);
//             currentWeapon = Instantiate(weapon, transform.position, transform.rotation);

//             currentWeapon.transform.parent = transform;
//             currentWeapon.transform.SetLocalPositionAndRotation(weaponOffset, Quaternion.identity);
//             currentWeapon.SetPlayer(this);

//             //Debug.Log(this.transform.forward);
//         }

//         public void Move(InputAction.CallbackContext ctx)
//         {
//             // Simple movement for testing purposes
//             input = ctx.ReadValue<Vector2>();
//             direction = new Vector3(input.x, 0.0f, input.y);
//         }

//         // Called when the player fires with LMB or RT on controller
//         virtual public void Fire(InputAction.CallbackContext ctx)
//         {
//             if (!currentWeapon)
//                 return;
//             rotationTimer = 250; // 50 FU's/sec -> 250/50 = 5 seconds
//         }

//         // Called when the player swaps weapons with Q or RB on controller
//         virtual public void SwapWeapon(InputAction.CallbackContext ctx)
//         {
//             // despawn current weapon
//             // add other weapon
//             // update currentWeapon
//             // reflect that on the UI

//             if (!currentWeapon)
//                 return;
//         }

//         public void OnHit(int dmg)
//         {
            
//             if (Time.time < hitTime)
//             {
//                 return;
//             }

//             hitTime = Time.time + invincibleSeconds;
//             //Debug.Log("health" + health);

//             health -= 10;
//             OnHealthChanged?.Invoke((float)health / 1); // Invoke the event, passing the current health percentage

//             if (health <= 0)
//             {
//                 OnHealthChanged?.Invoke((float)health / 1); // Invoke the event, passing the current health percentage

//                 OnDeath();
//                 return;
//             }

//             Sequence sequence = DOTween.Sequence();
//             sequence.Append(material.DOColor(Color.red, 0.2f));
//             sequence.Append(material.DOColor(originalColor, 0.2f));
//             sequence.Play();
//         }

//         void OnDeath()
//         {
//             Destroy(this.gameObject);
//         }
//         abstract protected void OnAwake();
//         abstract protected Vector3 GetMoveInput();

//         public Vector3 GetScreenCoordinates()
//         {
//             Vector3 weaponPos = cam.WorldToScreenPoint(transform.position);
//             // The z corrdinate is the distance from the camera to the player, so we don't care about that.
//             // Note that these axes would (most likely) change if we were to rotate the whole world along the X or Z axes (the ones not perpendicular to the ground)
//             weaponPos = new(weaponPos.x - (Screen.width / 2), 0, weaponPos.y - (Screen.height / 2));
//             return weaponPos.normalized;
//         }

//         public Vector3 GetScreenCoordinatesNotNormalized()
//         {
//             Vector3 weaponPos = cam.WorldToScreenPoint(transform.position);
//             // The z corrdinate is the distance from the camera to the player, so we don't care about that.
//             // Note that these axes would (most likely) change if we were to rotate the whole world along the X or Z axes (the ones not perpendicular to the ground)
//             weaponPos = new(weaponPos.x - (Screen.width / 2), 0, weaponPos.y - (Screen.height / 2));
//             return weaponPos;
//         }

//         public virtual void SetIsHammerSuper(bool status)
//         {
//             return;
//         }

//         public virtual void SetIsBlasterSuper(bool status)
//         {
//             return;
//         }

//         public override void OnNetworkSpawn()
//         {
//             if (!IsOwner) Destroy(this);
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Weapons;
using DG.Tweening;
// using UnityEditor.Animations;
using Sequence = DG.Tweening.Sequence;
using System;
using Unity.Netcode;

namespace Players
{
    abstract public class PlayerTestScript : NetworkBehaviour
    {
        // input
        protected Inputs inputs;
        Vector2 moveInput;

        // components
        Rigidbody body;

        // member variables
        [SerializeField]
        private int health = 100;

        // movement
        [SerializeField, Range(1, 10)]
        float maxVelocity = 5;

        [SerializeField, Range(1, 50)]
        float acceleration = 10;
        Vector3 desiredVelocity = new(0, 0, 0);
        float diagonalRegulator = 1;

        // weapons
        CharacterController characterController;
        Vector3 direction;
        protected Animator animator;
        protected Weapon currentWeapon;
        public event Action<float> OnHealthChanged; //changing health bar

        //How many seconds until player can get damaged again
        [SerializeField]
        float invincibleSeconds;
        float hitTime = 0;

        [SerializeField]
        float speed = 8;

        [SerializeField]
        float smoothTime = 0.05f;
        float currentVelocity;

        int rotationTimer = 0;

        public GameObject camObject; // this should be the cam specific to this player
        protected Camera cam;

        //Change player color to see it getting damaged
        Renderer _renderer;
        Material material;
        Color originalColor;

        // Start is called before the first frame update
        void Awake()
        {
            body = GetComponent<Rigidbody>();
            // inputs - IMPORTANT: any time you subscribe an input action to a function, unsubscribe it in OnDestroy.
            // Otherwise, when you reload the scene, the input will still be subscribed the function in the destroyed object,
            // and using the input will cause a NullObjectReference error and subsequently crash the game.
            inputs = new Inputs();
            inputs.Player.Enable();

            characterController = GetComponent<CharacterController>();

            cam = camObject.GetComponent<Camera>();
            OnAwake();
            //AttachWeapon(flamethrower); //giving player a flamethrower to test

            //For Changing Color when hit
            _renderer = GetComponent<Renderer>();
            material = Instantiate(_renderer.material);
            _renderer.material = material;
            originalColor = material.color;

            animator = GetComponent<Animator>();

            inputs.Player.Fire.performed += Fire;
            inputs.Player.SwapWeapon.performed += SwapWeapon;
            inputs.Player.Fire.canceled += Fire;
        }

        private void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            Debug.Log("HH1");
            NetworkObject NO = GetComponent<NetworkObject>();
            Debug.Log("HH2");
            Debug.Log(this.NetworkObjectId);
            Debug.Log("HH3");
            GetComponent<NetworkObject>().ChangeOwnership(this.NetworkObjectId);
            Debug.Log("HH4");
            // transform.position = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
        }

        // Update is called once per frame
        void Update()
        {
            moveInput = inputs.Player.Move.ReadValue<Vector2>();
            direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
        }

        void FixedUpdate()
        {
            if (!IsOwner)
                return;

            // desiredVelocity = new(moveInput.x, 0.0f, moveInput.y);

            // if (moveInput.x != 0 && moveInput.y != 0)
            //     diagonalRegulator = 2;
            // else
            //     diagonalRegulator = 1;
            // // normalize input values
            // if (moveInput.x < 0)
            //     desiredVelocity.x =
            //         -1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.x;
            // else if (moveInput.x > 0)
            //     desiredVelocity.x =
            //         1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.x;
            // else
            //     desiredVelocity.x = 0;

            // if (moveInput.y < 0)
            //     desiredVelocity.z =
            //         -1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.z;
            // else if (moveInput.y > 0)
            //     desiredVelocity.z =
            //         1 * acceleration * Time.fixedDeltaTime / diagonalRegulator + body.velocity.z;
            // else
            //     desiredVelocity.z = 0;

            desiredVelocity = new(
                moveInput.x * acceleration * Time.fixedDeltaTime + body.velocity.x,
                0,
                moveInput.y * acceleration * Time.fixedDeltaTime + body.velocity.z
            );

            // clamping
            desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxVelocity);
            // If the desired velocity is really low, just set it to zero (avoid appearance of 'lagging animation')
            // if (desiredVelocity.magnitude < 0.25f)
            //     desiredVelocity = Vector3.zero;

            if (animator)
            {
                if (desiredVelocity.magnitude != 0)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        animator.SetTrigger("OnRun");
                }
                else
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
                        animator.SetTrigger("OnIdle");
                }
            }

            // apply movements to rigidbody
            body.velocity = desiredVelocity;

            // if you have fired within the past 5 seconds, rotate to look in the direction of fire
            if (rotationTimer > 0)
                direction = -(
                    GameManager.GetMousePosition3NotNormalized()
                    - GetScreenCoordinatesNotNormalized()
                ).normalized;

            // if you have fired recently or you have put in a move input, rotate
            if (rotationTimer > 0 || direction != Vector3.zero)
            {
                var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
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

            //Debug.Log("Weapon offset: " + weaponOffset);
            currentWeapon = Instantiate(weapon, transform.position, transform.rotation);

            currentWeapon.transform.parent = transform;
            currentWeapon.transform.SetLocalPositionAndRotation(weaponOffset, Quaternion.identity);
            currentWeapon.SetPlayer(this);

            //Debug.Log(this.transform.forward);
        }

        // public void Move(InputAction.CallbackContext ctx)
        // {
        //     // Simple movement for testing purposes
        //     moveInput = ctx.ReadValue<Vector2>();
        //     direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
        // }

        // Called when the player fires with LMB or RT on controller
        virtual public void Fire(InputAction.CallbackContext ctx)
        {
            if (!currentWeapon)
                return;
            rotationTimer = 250; // 50 FU's/sec -> 250/50 = 5 seconds
            // if (ctx.performed)
            // {
            //     currentWeapon.OnFire();
            // }
            // else if (ctx.canceled)
            //     currentWeapon.StopFire();
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

        public void OnHit(int dmg)
        {
            if (Time.time < hitTime)
            {
                return;
            }

            hitTime = Time.time + invincibleSeconds;
            //Debug.Log("health" + health);

            health -= 10;
            OnHealthChanged?.Invoke((float)health / 1); // Invoke the event, passing the current health percentage

            if (health <= 0)
            {
                OnHealthChanged?.Invoke((float)health / 1); // Invoke the event, passing the current health percentage

                OnDeath();
                return;
            }

            Sequence sequence = DOTween.Sequence();
            sequence.Append(material.DOColor(Color.red, 0.2f));
            sequence.Append(material.DOColor(originalColor, 0.2f));
            sequence.Play();
        }

        void OnDeath()
        {
            Destroy(this.gameObject);
        }

        abstract protected void OnAwake();

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

        public virtual void SetIsHammerSuper(bool status)
        {
            return;
        }

        public virtual void SetIsBlasterSuper(bool status)
        {
            return;
        }

        public override void OnNetworkSpawn()
        {
            Debug.Log("H1");
            base.OnNetworkSpawn();
            Debug.Log("H2");
            Initialize();
            Debug.Log("H3");
            if (!IsOwner)
                Destroy(this);
            Debug.Log("H4");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            inputs.Player.Fire.performed -= Fire;
            inputs.Player.SwapWeapon.performed -= SwapWeapon;
            inputs.Player.Fire.canceled -= Fire;
        }
    }
}
