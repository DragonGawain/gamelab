using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;

namespace Weapons
{
    public class BulletBarrage : Weapon
    {
        static int deathTimer = 150;

        // Start is called before the first frame update
        void Start()
        {
            SetDamage(50);
            SetWeaponName("BulletBarrage");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ComboEnemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.OnHit(GetDamage(), "DarkPlayer", this);
            }
        }
        public override void OnFire()
        {

        }

        public override void StopFire()
        {

        }


    }
}
