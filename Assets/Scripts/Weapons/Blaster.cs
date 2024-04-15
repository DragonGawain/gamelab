using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using Weapons;

namespace Weapons
{
    public class Blaster : Weapon
    {
        [SerializeField]
        protected ParticleSystem particles;
        [SerializeField] private VisualEffect vfx;

        protected Transform firePoint;

        [SerializeField]
        protected GameObject bulletObject;

        [SerializeField]
        private static float bulletForce = 40f;

        protected static int fireDelay = 0;

        // Start is called before the first frame update
        void Start()
        {
            SetWeaponName("Blaster");
            // This is not a 'safe' way, but it will work. Just make sure that the fire point is alwayse the 0th child.
            firePoint = transform.GetChild(0);
            
            vfx.Stop();
        }

        private void FixedUpdate()
        {
            if (fireDelay >= 0)
                fireDelay--;
        }

        public override void OnFire()
        {
            if (fireDelay > 0)
                return;
            fireDelay = 25;
            // Creating bullet and making it go
            GameObject bullet = Instantiate(bulletObject, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // rb.velocity = GameManager.GetMousePosition3() * bulletForce; // TODO:: this may not work over the network
            // target - source

            rb.AddForce(
                (
                    GameManager.GetMousePosition3NotNormalized()
                    - player.GetScreenCoordinatesNotNormalized()
                ).normalized * bulletForce,
                ForceMode.Impulse
            );
            
            // playing vfx
            vfx.Play();
        }

        public override void StopFire()
        {
            return;
        }

        public static float GetBulletForce()
        {
            return bulletForce;
        }
    }
}
