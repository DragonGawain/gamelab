using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Players;
using TMPro;
using UnityEngine.UI;
using Weapons;
using Unity.VisualScripting;

namespace Players
{
    public class NetworkDarkPlayer : PlayerTestScript
    {
        //Prefabs of the weapons
        // [SerializeField]
        // private Hammer hammer;

        [SerializeField]
        private Hammer superHammer;

        [SerializeField]
        Hammer hammer; // I do not know why, but this variable wants to be named this :shrug:. It magically breaks if I rename it

        [SerializeField]
        private GrenadeLauncher grenadeLauncher;
        static bool isHammerSuper = false;

        //Attach weapon at start of game
        protected override void OnAwake()
        {
            // Debug.Log("HAMMER TIME: " + hammer);
            // Debug.Log("SUPER HAMMER TIME: " + superHammer);
            // Debug.Log("GRENADE TIME: " + grenadeLauncher);
            // Debug.Log("ygbyauda TIME: " + ygbyauda);
            AttachWeapon(hammer, new(-0.3f, 0, 0));
            ComboAttackManager.SetDarkPlayer(this);
            // inputs.Player.DarkFire.performed += DarkFire;
            // inputs.Player.DarkSwap.performed += DarkSwap;
        }

        // private void OnDestroy()
        // {
        //     inputs.Player.DarkFire.performed -= DarkFire;
        //     inputs.Player.DarkSwap.performed -= DarkSwap;
        // }

        public override void Fire(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            //Debug.Log(currentWeapon);
            base.Fire(ctx);
            currentWeapon.OnFire();

            // If they fire with the Hammer
            if (currentWeapon as StdHammer)
            {
                // Player holds to fire
                if (ctx.performed)
                {
                    //Debug.Log("on hammer attack");
                    //this.animator.SetTrigger("OnHammerAttack");
                    (currentWeapon as StdHammer).OnFire();
                }
            }
            else if (currentWeapon as SuperHammer)
            {
                // Player holds to fire
                if (ctx.performed)
                    (currentWeapon as SuperHammer).OnFire();
            }
            // If they fire with the grenadeLauncher
            else
            {
                if (ctx.performed)
                    (currentWeapon as GrenadeLauncher).OnFire();
            }
        }

        public override void SwapWeapon(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Debug.Log("dark swapped");

            base.SwapWeapon(ctx);

            // Despawn current weapon
            Destroy(currentWeapon.gameObject);
            // If the current weapon is the hammer
            if (currentWeapon as Hammer)
            {
                // Despawn current weapon

                // (currentWeapon as Hammer).Destroy();

                // Switch to the Grenade Launcher
                AttachWeapon(grenadeLauncher, new(-1.1f, 3, -0.75f));
            }
            else
            {
                // Switch to the hammer
                AttachWeapon(hammer, new(-0.3f, 0, 0));
            }
        }

        public override void SetIsHammerSuper(bool status)
        {
            bool oldStatus = isHammerSuper;
            isHammerSuper = status;
            if (oldStatus == isHammerSuper)
            {
                if (isHammerSuper)
                    SuperHammer.ResetSuperHammerTimer();
                else
                    return;
            }
            else if (!currentWeapon.GetWeaponName().Equals("Grenade Launcher"))
            {
                Destroy(currentWeapon.gameObject);
                if (isHammerSuper)
                {
                    AttachWeapon(superHammer, new(-0.3f, 0, 0));
                    SuperHammer.ResetSuperHammerTimer();
                }
                else
                    AttachWeapon(hammer, new(-0.3f, 0, 0));
            }
        }
    }
}
