using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Weapons{
    public class Blaster : Weapon
    {

        [SerializeField] private ParticleSystem particles;
        
        // Start is called before the first frame update
        void Start()
        {
            SetDamage(10);
            SetWeaponName("Blaster");
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

