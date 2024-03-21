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
    public class DarkPlayer : PlayerTestScript
    {
        //Prefabs of the weapons
        // [SerializeField]
        // private Hammer hammer;

        [SerializeField]
        private Hammer superHammer;

        [SerializeField]
        Hammer ygbyauda; // I do not know why, but this variable wants to be named this :shrug:. It magically breaks if I rename it

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
            AttachWeapon(ygbyauda, new(-0.3f, 0, 0));
            ComboAttackManager.SetDarkPlayer(this);
            physicalInputs.Player.DarkFire.performed += DarkFire;
            physicalInputs.Player.DarkSwap.performed += DarkSwap;
        }

        private void OnDestroy()
        {
            physicalInputs.Player.DarkFire.performed -= DarkFire;
            physicalInputs.Player.DarkSwap.performed -= DarkSwap;
        }

        // TODO:: ZAID, THIS MESSAGE IS DIRECTED SPECIFICALLY AT YOU (I know it was you :evil:)
        // Please add the override tag when you override a method. It tells your fellow programers important information!
        // Also, try to code things in a way that you don't need to do this. You can just, instantiate the hammerPrefab without setting all the stuff beforehand.
        // If you need it to be at a specific spot, modify the AttachWeapon method in the parent to take in a Vector3 for the position, and give it a default value
        // for the cases where you don't want to pass in a value.
        // Also, a quick test showed me that this not-override happens after the base method that you're not-overriding.
        // That means that generic stuff that I try to do in the base method doesn't happen here, so now I have code duplication.
        // TLDR: This here, bad code practices. Leads to higher maintenance costs (takes more time to set up the code duplication, and time to find all the places where the code has to be duplicated)
        // public void AttachWeapon(Weapon weapon)
        // {
        //     Debug.Log("internal dark swap");
        //     if (weapon as Hammer)
        //     {
        //         GameObject handle = new GameObject("Hammer Handle");
        //         handle.transform.parent = transform;
        //         handle.transform.localRotation = Quaternion.identity;
        //         handle.transform.localPosition = new Vector3(-0.5f, -1f, 0);

        //         currentWeapon = Instantiate(hammer, handle.transform);
        //         currentWeapon.transform.localPosition = new Vector3(-0.3f, 1.0f, 0);
        //     }
        //     else
        //     {
        //         currentWeapon = Instantiate(grenadeLauncher, transform);
        //         currentWeapon.transform.localPosition = new Vector3(-1.1f, 1, -0.75f);
        //     }
        //     // This line is my code duplication btw. It may look simple, but it was causing the program to crash cause I didn't know you were overriding the method, and the lack of the override tag made my eyes skip over it a few time, costing me time.
        //     currentWeapon.SetPlayer(this);
        // }

        public void DarkFire(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            //Debug.Log(currentWeapon);
            base.Fire(ctx);
            currentWeapon.OnFire();
            // If they fire with the Hammer
            if (currentWeapon as StdHammer)
            {
                // Player holds to fire
                if (ctx.performed)
                    (currentWeapon as StdHammer).OnFire();
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

        public void DarkSwap(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
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
                AttachWeapon(grenadeLauncher, new(-1.1f, 1, -0.75f));
            }
            else
            {
                // Switch to the hammer
                AttachWeapon(ygbyauda, new(-0.3f, 0, 0));
            }
        }

        protected override Vector3 GetMoveInput()
        {
            Vector2 moveInput = physicalInputs.Player.DarkMove.ReadValue<Vector2>();
            Vector3 dir = new(moveInput.x, 0.0f, moveInput.y);
            return dir;
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
                    AttachWeapon(ygbyauda, new(-0.3f, 0, 0));
            }
        }
    }
}
