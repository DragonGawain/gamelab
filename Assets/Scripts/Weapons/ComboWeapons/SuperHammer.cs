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
            SetDamage(50);
            SetWeaponName("SuperHammer");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            deathTimer--;
            if (deathTimer <= 0)
            {
                // instance = null;
                player.SetIsHammerSuper(false);
                // Destroy(this.gameObject);
            }
        }

        public static void ResetSuperHammerTimer()
        {
            deathTimer = 450;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object is tagged as "Enemy2"
            if (other.CompareTag("ComboEnemy"))
            {
                // Attempt to get the Enemy component from the collided object
                Enemy enemy = other.GetComponent<Enemy>();

                // Check if the Enemy component was successfully retrieved
                if (enemy != null)
                {
                    // Now that you have a reference to the Enemy component, you can call its OnHit method
                    enemy.OnHit(GetDamage(), "DarkPlayer", this);
                }
                else
                {
                    // Log a message or handle the case where the Enemy component is missing
                    Debug.LogWarning("Enemy component not found on object tagged as 'Enemy2'");
                }
            }
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
