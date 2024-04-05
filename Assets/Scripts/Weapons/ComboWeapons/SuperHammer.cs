using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;

namespace Weapons
{
    public class SuperHammer : Hammer
    {
        static int deathTimer = 450;

        // Start is called before the first frame update
        void Start()
        {
            dmg = 50;
            SetWeaponName("SuperHammer");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            deathTimer--;
            if (deathTimer <= 0)
                player.SetIsHammerSuper(false);
        }

        public static void ResetSuperHammerTimer()
        {
            deathTimer = 450;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object is tagged as "Enemy2"
            if (hit && other.CompareTag("ComboEnemy"))
                other.GetComponent<Enemy>().OnHit(dmg, "DarkPlayer");
        }

        protected override void slam()
        {
            if (deathTimer <= 25)
                deathTimer = 25;
            base.slam();
        }

        public override void StopFire()
        {
            base.StopFire();
            if (deathTimer <= 10)
                deathTimer = 1;
        }
    }
}
