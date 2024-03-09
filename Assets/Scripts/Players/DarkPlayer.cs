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
    public class DarkPlayer : PlayerTestScript
    {
        [SerializeField] private Hammer hammer;
        [SerializeField] private GrenadeLauncher grenadeLauncher;
        
        
        public TextMeshProUGUI text;

        // This gets called in the Awake() function of the parent class
        protected override void OnAwake()
        {
            // Start with this weapon
            
            AttachWeapon(hammer); 
        }
        
        public void AttachWeapon(Weapon weapon){
            if (weapon as Hammer)
            {
                GameObject handle = new GameObject("Hammer Handle");
                handle.transform.parent = transform;
                handle.transform.localRotation = Quaternion.identity;
                handle.transform.localPosition = new Vector3(-0.5f, -1f, 0);

                currentWeapon = Instantiate(hammer, handle.transform);
                currentWeapon.transform.localPosition = new Vector3(-0.3f,1.0f,0);
                
            }

            else
            {
                currentWeapon = Instantiate(grenadeLauncher, transform);
                currentWeapon.transform.localPosition = new Vector3(-1.1f, 1, -0.75f);
            }
        }
        public override void Fire(InputAction.CallbackContext ctx)
        {
            base.Fire(ctx);
            currentWeapon.OnFire();
            // If they fire with the Hammer
            if (currentWeapon as Hammer)
            {
                // Player holds to fire
                if (ctx.performed)
                {
                    (currentWeapon as Hammer).OnFire();
                }

            }
            
            // If they fire with the grenadeLauncher
            else
            {
                if (ctx.performed)
                {
                    (currentWeapon as GrenadeLauncher).OnFire();
                }
            }
        }

        public override void SwapWeapon(InputAction.CallbackContext ctx)
        {
            
            base.SwapWeapon(ctx);

            // If the current weapon is the hammer
            if (currentWeapon as Hammer)
            {
                // Despawn current weapon

                (currentWeapon as Hammer).Destroy();
                
                // Switch to the Grenade Launcher
                AttachWeapon(grenadeLauncher);
                
            }
            else
            {
                // Despawn current weapon
                Destroy(currentWeapon.gameObject);
                
                // Switch to the hammer
                AttachWeapon(hammer);
            }
            
        }
        
    }

}
