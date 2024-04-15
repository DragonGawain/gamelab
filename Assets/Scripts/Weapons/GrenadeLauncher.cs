using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class GrenadeLauncher : Weapon
    {
        // Start is called before the first frame update

        [SerializeField]
        private float angle;

        [SerializeField]
        private float grenadeForce = 8;

        [SerializeField]
        private GameObject grenadePrefab;

        [SerializeField]
        private float fireRate;

        private AudioSource soundEffect;

        //because of multiple inputs;
        private float fireTime;

        void Start()
        {
            transform.localEulerAngles = new Vector3(0, -90, 0);
            SetWeaponName("GrenadeLauncher");
            soundEffect = GetComponent<AudioSource>();
        }

        public override void OnFire()
        {
            if (Time.time > fireTime)
            {
                soundEffect.Play();
                GameObject grenade = Instantiate(
                    grenadePrefab,
                    transform.position,
                    transform.rotation
                );
                Grenade g = grenade.GetComponent<Grenade>();
                g.SetWeaponRef(this);

                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                // Debug.Log("GRENADE RB: " + rb);
                rb.AddForce(
                    transform.right * grenadeForce, // Use transform.forward instead of mouse direction
                    ForceMode.Impulse
                );
                fireTime = fireRate + Time.time;
            }
        }

        public override void StopFire() { }
    }
}
