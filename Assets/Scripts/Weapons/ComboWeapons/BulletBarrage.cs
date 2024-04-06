using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;

namespace Weapons
{
    // Inherits Bullet class just for the animation
    public class BulletBarrage : Bullet
    {
        readonly int dmg = 50;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ComboEnemy"))
            {
                other.GetComponent<Enemy>().OnHit(dmg, "DarkPlayer");
                Destroy(this.gameObject);
            }
        }
    }
}
