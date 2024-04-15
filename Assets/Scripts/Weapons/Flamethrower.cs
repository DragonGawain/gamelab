using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using Weapons;

namespace Weapons
{
    public class Flamethrower : Weapon
    {
       
        [SerializeField] private VisualEffect vfx;

        [SerializeField]
        private CapsuleCollider flameHitbox;
        public bool firing;

        // Start is called before the first frame update
        void Start()
        {
            SetWeaponName("Flamethrower");
            vfx.Stop();
        }

        public override void OnFire()
        {
            firing = true;
            vfx.Play();
            flameHitbox.enabled = true;
        }

        public override void StopFire()
        {
            firing = false;
            vfx.Stop();
            flameHitbox.enabled = false;
        }
    }
}
