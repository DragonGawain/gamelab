using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;

namespace Weapons
{
    public class SuperBullet : Bullet
    {
        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ComboEnemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.OnHit(weaponRef.GetDamage(), "LightPlayer", weaponRef);

            }
        }


    }
}
