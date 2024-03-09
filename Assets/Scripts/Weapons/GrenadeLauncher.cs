using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace  Weapons
{
    public class GrenadeLauncher : Weapon
    {
        // Start is called before the first frame update
        
        [SerializeField] private float angle;
        [SerializeField] private float gravity;
        [SerializeField] private float speed;
        [SerializeField] private Grenade grenadePrefab;
        [SerializeField] private float fireRate;
        //because of multiple inputs;
        private float fireTime;
        void Start()
        {
            int dmg = 10;
            SetDamage(dmg);
            Grenade.damage = dmg;
            transform.localEulerAngles = new Vector3(0, -90, -angle);
        }

        public override void OnFire()
        {
            
            if (Time.time > fireTime)
            {
                Grenade grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
                fireTime = fireRate + Time.time;
            }
            

        }

        public override void StopFire()
        {
            
        }
    }
    
}
