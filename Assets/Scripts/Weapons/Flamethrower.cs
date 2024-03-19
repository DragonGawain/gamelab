using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Weapons{
    public class Flamethrower : Weapon
    {

        [SerializeField] private ParticleSystem particles;
        [SerializeField] private CapsuleCollider flameHitbox;
        
        // Start is called before the first frame update
        void Start()
        {
            SetDamage(15);
            SetWeaponName("Flamethrower");
        }
        
        public override void OnFire()
        {
            particles.Play();
            flameHitbox.enabled = true;
        }

        public override void StopFire()
        {
            particles.Stop();
            flameHitbox.enabled = false;
        }
    }
}

