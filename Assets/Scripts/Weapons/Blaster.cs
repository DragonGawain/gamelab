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
        private ParticleSystem particles;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private GameObject bulletPrefab;

        [SerializeField]
        private static float bulletForce = 40f;

        // Start is called before the first frame update
        void Start()
        {
            SetDamage(10);
            SetWeaponName("Blaster");
        }

        public override void OnFire()
        {
            // Creating bullet and making it go
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            // rb.velocity = GameManager.GetMousePosition3() * bulletForce; // TODO:: this may not work over the network
            rb.AddForce(GameManager.GetMousePosition3() * bulletForce, ForceMode.Impulse);
        }

        public override void StopFire()
        {
            //
        }

        public static float GetBulletForce()
        {
            return bulletForce;
        }
    }
}
