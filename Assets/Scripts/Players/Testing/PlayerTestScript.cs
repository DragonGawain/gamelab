using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Weapons;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;
using System;
using Unity.Netcode;
using UnityEngine.VFX;

namespace Players
{
    abstract public class PlayerTestScript : NetworkBehaviour
    {
        private Vector2 input;
        private Vector3 direction;
        protected Animator animator;
        protected Weapon currentWeapon;
        [SerializeField] protected VisualEffect vfx;
        [SerializeField] protected VisualEffect dustFX;
        [SerializeField] protected AudioSource walkingSound;
        
        public event Action<float> OnHealthChanged; //changing health bar

        //Network stuff
        [SerializeField]
        GameObject lightPlayer;

        [SerializeField]
        GameObject darkPlayer;
        public static int selection = 1;

        [SerializeField]
        private int health;

        //How many seconds until player can get damaged again
        [SerializeField]
        private float invincibleSeconds;
        private float hitTime = 0;

        [SerializeField]
        private float smoothTime = 0.05f;

        protected Inputs physicalInputs;

        protected int rotationTimer = 0;

        public GameObject camObject; // this should be the cam specific to this player
        protected Camera cam;

        //Change player color to see it getting damaged
        private Renderer _renderer;
        private Material material;
        private Color originalColor;

        protected Rigidbody body;

        // movement
        // [SerializeField, Range(1, 10)]
        float maxVelocity = 7;

        [SerializeField, Range(1, 50)]
        protected float acceleration = 10;
        protected Vector3 desiredVelocity = new(0, 0, 0);
        private float currentVelocity;

        private void Awake()
        {
            vfx.Stop();
            dustFX.Stop();
            
            physicalInputs = new Inputs();
            physicalInputs.Player.Enable();
            // cam = camObject.GetComponent<Camera>();
            cam = Camera.main;
            OnAwake();

            //For Changing Color when hit
            _renderer = GetComponent<Renderer>();
            material = Instantiate(_renderer.material);
            _renderer.material = material;
            originalColor = material.color;

            animator = GetComponent<Animator>();
            body = GetComponent<Rigidbody>();

            physicalInputs.Player.Fire.performed += Fire;
            physicalInputs.Player.Fire.canceled += Fire;
            physicalInputs.Player.SwapWeapon.performed += SwapWeapon;
        }

        private void Start()
        {
            vfx.Stop();
            dustFX.Stop();
        }

        private void FixedUpdate()
        {
            // Slow down the body
            if (body.velocity.magnitude >= 0.1f)
                body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.75f);
            else
                body.velocity = Vector3.zero;
            desiredVelocity = GetMoveInput();
            if (desiredVelocity.magnitude != 0)
            {
                desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxVelocity);
                body.velocity = desiredVelocity;
                if (animator != null)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        animator.SetTrigger("OnRun");

                    if (!walkingSound.isPlaying)
                    {
                        walkingSound.Play();
                    }

                    dustFX.Play();
                    
                }
            }
            else
            {
                if (animator != null)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
                        animator.SetTrigger("OnIdle");
                    
                    walkingSound.Stop();
                    dustFX.Stop();
                }
            }

            // if you have fired within the past 5 seconds, rotate to look in the direction of fire
            if (rotationTimer > 0)
            {
                direction = (
                    GameManager.GetMousePosition3NotNormalized()
                    - GetScreenCoordinatesNotNormalized()
                ).normalized;
            //     Debug.Log(
            //         "Mouse pos: "
            //             + GameManager.GetMousePosition3NotNormalized()
            //             + "screen coords: "
            //             + GetScreenCoordinatesNotNormalized()
            //     );
            }
            else
                direction = desiredVelocity.normalized;

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
        protected void AttachWeapon(
            Weapon weapon,
            Vector3? weaponOffsetInput = null,
            bool isHammer = false
        )
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

            currentWeapon = Instantiate(weapon, transform.position, transform.rotation);

            currentWeapon.transform.parent = transform;
            currentWeapon.transform.SetLocalPositionAndRotation(weaponOffset, Quaternion.identity);
            // Flip the rotations of all the weapons (they need it) except the hammers cause they're special
            // This is the bad kind of special btw. It's because the rotation sequence has hard-coded values and will cause the hammer to teleport,
            // so we need to set it's position to match.

            // Since the object is only just initialized, we cannot check the name of the weapon (it has only just now been set)
            // So, I'm instead passing in *another* default var to check
            if (!(isHammer))
                currentWeapon.transform.Rotate(new(0, 1, 0), 180);

            currentWeapon.SetPlayer(this);
        }

        // Called when the player fires with LMB or RT on controller
        virtual public void Fire(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if (!currentWeapon)
                return;
            rotationTimer = 250; // 50 FU's/sec -> 250/50 = 5 seconds
        }

        // Called when the player swaps weapons with Q or RB on controller
        virtual public void SwapWeapon(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
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

        public void Revive()
        {
            health = 100;
        }

        public int GetHealth()
        {
            return health;
        }

        abstract protected void OnDeath();
        abstract protected void OnAwake();

        virtual protected Vector3 GetMoveInput()
        {
            Vector2 moveInput = physicalInputs.Player.Move.ReadValue<Vector2>();
            Vector3 dir =
                new(
                    moveInput.x * acceleration + body.velocity.x,
                    0.0f,
                    moveInput.y * acceleration + body.velocity.z
                );
            return dir;
        }

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
            /*if (IsServer)
            {
                Debug.Log("Is host");
                selection = 0;
                //NetworkManager.GetNetworkPrefabOverride(GameObject.FindGameObjectWithTag("DarkPlayer"));
                NetworkManager.GetNetworkPrefabOverride(darkPlayer);
            }
            if (IsOwner && selection == 1)
            {
                Debug.Log("is Client");
                //NetworkManager.GetNetworkPrefabOverride(GameObject.FindGameObjectWithTag("LightSelect"));
                NetworkManager.GetNetworkPrefabOverride(lightPlayer);
            }*/

            if (!IsOwner)
            {
                enabled = false;
                return;
            }

            //Set camera to follow this player
            CameraFollow cameraFollow = cam.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.enabled = true;
                cameraFollow.SetPlayer(this.transform);
            }

            if (IsServer) { }
            else { }
            //Destroy(this);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            physicalInputs.Player.Fire.performed += Fire;
            physicalInputs.Player.Fire.canceled += Fire;
            physicalInputs.Player.SwapWeapon.performed += SwapWeapon;
        }
    }
}
