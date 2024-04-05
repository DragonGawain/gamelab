using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class Flamethrower : Weapon
    {
        [SerializeField]
        private ParticleSystem particles;

        [SerializeField]
        private CapsuleCollider flameHitbox;
        public bool firing;

        // Start is called before the first frame update
        void Start()
        {
            SetWeaponName("Flamethrower");
        }

        public override void OnFire()
        {
            firing = true;
            particles.Play();
            flameHitbox.enabled = true;
        }

        public override void StopFire()
        {
            firing = false;
            particles.Stop();
            flameHitbox.enabled = false;
        }
    }
}
