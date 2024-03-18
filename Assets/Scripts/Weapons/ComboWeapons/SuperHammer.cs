using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;

namespace Weapons
{
    public class SuperHammer : Hammer
    {
        static int deathTimer = 150;

        // Start is called before the first frame update
        void Start()
        {
            SetDamage(50);
            SetWeaponName("Super Hammer");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            deathTimer--;
            if (deathTimer <= 0)
            {
                // instance = null;
                player.SetIsHammerSuper(false);
                Destroy(this.gameObject);
            }
        }

        public static void ResetSuperHammerTimer()
        {
            deathTimer = 150;
        }
    }
}
