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


        [SerializeField] private int dmg;
        
        [SerializeField]
        private float grenadeForce = 8;

        [SerializeField]
        private GameObject grenadePrefab;

        [SerializeField]
        private float fireRate;

        //because of multiple inputs;
        private float fireTime;

        void Start()
        {
            SetDamage(dmg);
            Grenade.dmg = dmg;
            transform.localEulerAngles = new Vector3(0, -90, -angle);
            SetWeaponName("Grenade Launcher");
        }

        public override void OnFire()
        {
            if (Time.time > fireTime)
            {
                GameObject grenade = Instantiate(
                    grenadePrefab,
                    transform.position,
                    transform.rotation
                );
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                Debug.Log("GRENADE RB: " + rb);
                rb.AddForce(
                    (
                        GameManager.GetMousePosition3NotNormalized()
                        - player.GetScreenCoordinatesNotNormalized()
                    ).normalized * grenadeForce,
                    ForceMode.Impulse
                );
                fireTime = fireRate + Time.time;
            }
        }

        public override void StopFire() { }
    }
}
