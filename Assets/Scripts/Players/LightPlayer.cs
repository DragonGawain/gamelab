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
    public class LightPlayer : PlayerTestScript
    {
        public Flamethrower flamethrower;
        public Blaster blaster;
        public TextMeshProUGUI text;

        // This gets called in the Awake() function of the parent class
        protected override void OnAwake()
        {
            // Start with this weapon
           AttachWeapon(flamethrower); 
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
            
            base.SwapWeapon(ctx);

            // If the current weapon is flamethrower
            if (currentWeapon as Flamethrower)
            {
                // Despawn current weapon
                Destroy(currentWeapon.gameObject);
                
                // Switch to blaster
                AttachWeapon(blaster);
                
                // Update UI
                UpdateText("Blaster");
                
            }
            else
            {
                // Despawn current weapon
                Destroy(currentWeapon.gameObject);
                
                // Switch to flamethrower
                AttachWeapon(flamethrower);
                
                // Update UI
                UpdateText("Flamethrower");
            }
            
        }

        // To be deleted later, this is just for show
        private void UpdateText(string weaponName)
        {
            text.text = weaponName;
            if (weaponName == "Flamethrower") text.color = Color.cyan;
            else text.color = Color.blue;
            
        }
    }

}
