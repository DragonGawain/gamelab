using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Weapons{
    public class Flamethrower : Weapon
    {

        [SerializeField] private ParticleSystem particles;
        
        // Start is called before the first frame update
        void Start()
        {
            SetDamage(15);
            SetWeaponName("Flamethrower");
        }
        
        public void Fire()
        {
            particles.Play();
        }

        public void StopFire()
        {
            particles.Stop();
        }
    }
}

