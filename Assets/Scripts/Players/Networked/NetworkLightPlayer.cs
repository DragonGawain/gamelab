using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Players;
using TMPro;
using UnityEngine.UI;
using Weapons;

namespace Players
{
    public class NetworkLightPlayer : PlayerTestScript
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
            // inputs.Player.LightFire.performed += LightFire;
            // inputs.Player.LightSwap.performed += LightSwap;
            // inputs.Player.LightFire.canceled += LightFire;

            ComboAttackManager.SetLightPlayer(this);
        }

        // private void OnDestroy()
        // {
        //     inputs.Player.LightFire.performed -= LightFire;
        //     inputs.Player.LightSwap.performed -= LightSwap;
        //     inputs.Player.LightFire.canceled -= LightFire;
        // }

        private void Update()
        {
            // Vector2 mousePos = inputs.Player.MousePos.ReadValue<Vector2>();
            // mousePos = new(mousePos.x - (Screen.width / 2), mousePos.y - (Screen.height / 2));
            // Debug.Log("mouse Pos: " + mousePos);
        }

        public override void Fire(InputAction.CallbackContext ctx)
        {
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

        public override void SwapWeapon(InputAction.CallbackContext ctx)
        {
            Debug.Log("light swapped");

            base.SwapWeapon(ctx);

            // Despawn current weapon
            Destroy(currentWeapon.gameObject);
            // If the current weapon is flamethrower
            if (currentWeapon as Flamethrower)
            {
                // Switch to blaster
                AttachWeapon(blaster);

                // Update UI
                // UpdateText("Blaster");
            }
            else
            {
                // Switch to flamethrower
                AttachWeapon(flamethrower);

                // Update UI
                // UpdateText("Flamethrower");
            }
        }

        public override void SetIsBlasterSuper(bool status)
        {
            bool oldStatus = isBlasterSuper;
            isBlasterSuper = status;
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
                    AttachWeapon(superBlaster);
                    SuperBlaster.ResetSuperBlasterTimer();
                }
                else
                    AttachWeapon(blaster);
            }
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

    }
}
