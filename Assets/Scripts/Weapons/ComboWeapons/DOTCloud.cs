using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;

namespace Weapons
{
    public class DOTCloud : Weapon
    {
        static int deathTimer = 150;

        private float blastRadius = 10;
        private bool isTakingDOTDamage = false;

        // Start is called before the first frame update
        void Start()
        {
            SetDamage(25);
            SetWeaponName("DOTCloud");
        }


        public void FixedUpdate()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius / 2);
            foreach (Collider hit in colliders)
            {
                if (!hit.CompareTag("ComboEnemy"))
                    continue;
                // Check if the collider belongs to an enemy
                Enemy enemy = hit.GetComponent<Enemy>();

                // Passing the weapon reference to the bullet so the enemy can handle weapon info


                enemy.OnHit(GetDamage(), "DarkPlayer", this);
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(100, transform.position, blastRadius);
                }
            }
        }




        public override void OnFire()
        {
            throw new System.NotImplementedException();
        }

        public override void StopFire()
        {
            throw new System.NotImplementedException();
        }
    }
}
