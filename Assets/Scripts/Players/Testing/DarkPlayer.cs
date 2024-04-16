using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Players;
using TMPro;
using Unity.Netcode;
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
        Hammer hammer;

        [SerializeField]
        private GrenadeLauncher grenadeLauncher;
        static bool isHammerSuper = false;

        //Attach weapon at start of game
        protected override void OnAwake()
        {
            AttachWeapon(hammer, new(-0.3f, 0, 0), true);
            // physicalInputs.Player.DarkFire.performed += DarkFire;
            // physicalInputs.Player.DarkSwap.performed += DarkSwap;

            ComboAttackManager.SetDarkPlayer(this);
            SO_TargetManager.darkPlayer = this;
            PlayerManager.SetDarkPlayer(this);
        }

        // public override void OnDestroy()
        // {
        //     base.OnDestroy();
        //     physicalInputs.Player.DarkFire.performed -= DarkFire;
        //     physicalInputs.Player.DarkSwap.performed -= DarkSwap;
        // }

        public override void Fire(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            //Prevent players from swapping each others weapons
            if (!IsOwner)
                return;

            base.Fire(ctx);
            RequestDarkFireServerRpc();
        }

        [ServerRpc]
        void RequestDarkFireServerRpc()
        {
            DarkFireClientRpc();
        }

        [ClientRpc]
        void DarkFireClientRpc()
        {
            if (currentWeapon as StdHammer)
            {
                (currentWeapon as StdHammer).OnFire();
            }
            else if (currentWeapon as SuperHammer)
            {
                (currentWeapon as SuperHammer).OnFire();
            }
            else
            {
                (currentWeapon as GrenadeLauncher).OnFire();
            }
        }

        public override void SwapWeapon(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            //Prevent players from swapping each others weapons
            if (!IsOwner)
                return;

            isHammerSuper = false;
            base.SwapWeapon(ctx);
            Debug.Log("dark swapped");
            RequestDarkSwapServerRpc();
        }

        [ServerRpc]
        void RequestDarkSwapServerRpc()
        {
            DarkSwapClientRpc();
        }

        [ClientRpc]
        void DarkSwapClientRpc()
        {
            // Despawn current weapon
            Destroy(currentWeapon.gameObject);

            // If the current weapon is the hammer
            if (currentWeapon as Hammer)
            {
                // Switch to the Grenade Launcher
                AttachWeapon(grenadeLauncher, new(-1.1f, 3, -0.75f));
            }
            else
            {
                // Switch to the hammer
                AttachWeapon(hammer, new(-0.3f, 0, 0), true);
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
                if (currentWeapon.GetWeaponName().Equals("Hammer"))
                {
                    if (Hammer.GetIsSwinging())
                    {
                        Hammer.SetShouldBeSuper();
                        isHammerSuper = false; // Need to set this to false so that the super hammer gets spawned
                        return;
                    }
                }
                Destroy(currentWeapon.gameObject);
                if (isHammerSuper)
                {
                    AttachWeapon(superHammer, new(-0.3f, 0, 0), true);
                    SuperHammer.ResetSuperHammerTimer();
                }
                else
                    AttachWeapon(hammer, new(-0.3f, 0, 0), true);
            }
        }

        protected override void OnDeath()
        {
            PlayerManager.OnDarkPlayerDeath();
        }
    }
}
