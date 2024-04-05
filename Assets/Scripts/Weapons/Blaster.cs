using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class Blaster : Weapon
    {
        [SerializeField]
        protected ParticleSystem particles;

        protected Transform firePoint;

        [SerializeField]
        protected GameObject bulletObject;

        [SerializeField]
        private static float bulletForce = 40f;

        // Start is called before the first frame update
        void Start()
        {
            SetWeaponName("Blaster");
            // This is not a 'safe' way, but it will work. Just make sure that the fire point is alwayse the 0th child.
            firePoint = transform.GetChild(0);
        }

        public override void OnFire()
        {
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
