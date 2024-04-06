using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;

namespace Weapons
{
    // Inherits Bullet class just for the animation
    public class SuperBullet : Bullet
    {
        readonly int dmg = 20;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ComboEnemy"))
            {
                other.GetComponent<Enemy>().OnHit(dmg, "LightPlayer");
                Destroy(this.gameObject);
            }
        }
    }
}
