using Unity.Netcode;
using UnityEngine;

namespace Weapons
{
    public class Bullet : NetworkBehaviour
    {
        readonly int dmg = 20;

        public void Explode()
        {
            // put an animation player here or smth instead of the bullet just disappearing
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BasicEnemy"))
            {
                other.GetComponent<Enemy>().OnHit(dmg, "LightPlayer");
                Destroy(this.gameObject);
            }
        }
    }
}
