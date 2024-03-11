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
        //Prefabs of the weapons
        [SerializeField]
        private Hammer hammer;

        [SerializeField]
        private GrenadeLauncher grenadeLauncher;

        //Attach weapon at start of game
        protected override void OnAwake()
        {
            AttachWeapon(hammer);
            physicalInputs.Player.DarkFire.performed += DarkFire;
            physicalInputs.Player.DarkSwap.performed += DarkSwap;
        }

        private void OnDestroy()
        {
            physicalInputs.Player.DarkFire.performed -= DarkFire;
            physicalInputs.Player.DarkSwap.performed -= DarkSwap;
        }

        public void AttachWeapon(Weapon weapon)
        {
            if (weapon as Hammer)
            {
                GameObject handle = new GameObject("Hammer Handle");
                handle.transform.parent = transform;
                handle.transform.localRotation = Quaternion.identity;
                handle.transform.localPosition = new Vector3(-0.5f, -1f, 0);

                currentWeapon = Instantiate(hammer, handle.transform);
                currentWeapon.transform.localPosition = new Vector3(-0.3f, 1.0f, 0);
            }
            else
            {
                currentWeapon = Instantiate(grenadeLauncher, transform);
                currentWeapon.transform.localPosition = new Vector3(-1.1f, 1, -0.75f);
            }
        }

        public void DarkFire(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
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

        public void DarkSwap(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Debug.Log("dark swapped");

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

        protected override Vector3 GetMoveInput()
        {
            Vector2 moveInput = physicalInputs.Player.DarkMove.ReadValue<Vector2>();
            Vector3 dir = new(moveInput.x, 0.0f, moveInput.y);
            return dir;
        }
    }
}
