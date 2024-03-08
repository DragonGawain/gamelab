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
        
        public override void OnFire()
        {
            particles.Play();
        }

        public override void StopFire()
        {
            particles.Stop();
        }
    }
}

