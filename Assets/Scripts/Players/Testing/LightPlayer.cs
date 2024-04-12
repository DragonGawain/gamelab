using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Players;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;
using Weapons;

namespace Players
{
    public class LightPlayer : PlayerTestScript
    {
        public Flamethrower flamethrower;
        public Blaster blaster;
        public SuperBlaster superBlaster;
        public TextMeshProUGUI text;

        static bool isBlasterSuper = false;

        // This gets called in the Awake() function of the parent class
        protected override void OnAwake()
        {
            // Start with this weapon
            AttachWeapon(flamethrower);
            // physicalInputs.Player.LightFire.performed += LightFire;
            // physicalInputs.Player.LightSwap.performed += LightSwap;
            // physicalInputs.Player.LightFire.canceled += LightFire;

            ComboAttackManager.SetLightPlayer(this);
            SO_TargetManager.lightPlayer = this;
            PlayerManager.SetLightPlayer(this);
        }

        // public override void OnDestroy()
        // {
        //     base.OnDestroy();
        //     physicalInputs.Player.LightFire.performed -= LightFire;
        //     physicalInputs.Player.LightSwap.performed -= LightSwap;
        //     physicalInputs.Player.LightFire.canceled -= LightFire;
        // }

        public override void Fire(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            //Prevent players from swapping each others weapons
            if (!IsOwner)
                return;

            base.Fire(ctx);

            // If they fire with the Flamethrower
            if (currentWeapon as Flamethrower)
            {
                // Player holds to fire
                if (ctx.performed)
                {
                    (currentWeapon as Flamethrower).OnFire();
                }
                // Player releases
                else if (ctx.canceled)
                {
                    (currentWeapon as Flamethrower).StopFire();
                }
            }
            // If they fire with the Blaster
            else
            {
                if (ctx.performed)
                {
                    (currentWeapon as Blaster).OnFire();
                }
            }
        }

        public override void SwapWeapon(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            //Prevent players from swapping each others weapons
            if (!IsOwner)
                return;

            Debug.Log("light swapped");
            isBlasterSuper = false;
            base.SwapWeapon(ctx);
            LightSwapServerRpc();
        }

        [ServerRpc]
        public void LightSwapServerRpc()
        {
            LightSwapClientRpc();
        }

        [ClientRpc]
        public void LightSwapClientRpc()
        {
            // Despawn current weapon
            Destroy(currentWeapon.gameObject);
            // If the current weapon is flamethrower
            if (currentWeapon as Flamethrower)
            {
                // Switch to blaster
                AttachWeapon(blaster, new(-0.85f, 2, 0));

                // Update UI
                // UpdateText("Blaster");
            }
            else
            {
                // Switch to flamethrower
                AttachWeapon(flamethrower, new(-0.85f, 2, 0));

                // Update UI
                // UpdateText("Flamethrower");
            }
        }

        public override void SetIsBlasterSuper(bool status)
        {
            bool oldStatus = isBlasterSuper;
            isBlasterSuper = status;
            Debug.Log(currentWeapon + ", " + currentWeapon.GetWeaponName());
            if (oldStatus == isBlasterSuper)
            {
                if (isBlasterSuper)
                    SuperBlaster.ResetSuperBlasterTimer();
                else
                    return;
            }
            else if (!currentWeapon.GetWeaponName().Equals("Flamethrower"))
            {
                Destroy(currentWeapon.gameObject);
                if (isBlasterSuper)
                {
                    AttachWeapon(superBlaster, new(-0.85f, 2, 0));
                    SuperBlaster.ResetSuperBlasterTimer();
                }
                else
                    AttachWeapon(blaster, new(-0.85f, 2, 0));
            }
        }

        protected override void OnDeath()
        {
            PlayerManager.OnLightPlayerDeath();
        }

        // To be deleted later, this is just for show
        private void UpdateText(string weaponName)
        {
            text.text = weaponName;
            if (weaponName == "Flamethrower")
                text.color = Color.cyan;
            else
                text.color = Color.blue;
        }

        // protected override Vector3 GetMoveInput()
        // {
        //     Vector2 moveInput = physicalInputs.Player.LightMove.ReadValue<Vector2>();
        //     Vector3 dir =
        //         new(
        //             moveInput.x * acceleration + body.velocity.x,
        //             0.0f,
        //             moveInput.y * acceleration + body.velocity.z
        //         );
        //     return dir;
        // }
    }
}
