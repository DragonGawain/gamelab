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
        
        [SerializeField] AudioSource firingLoopSound;
        [SerializeField] private AudioSource startupSound;
        

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
            startupSound.Play();
            firingLoopSound.Play();
            flameHitbox.enabled = true;
        }

        public override void StopFire()
        {
            firing = false;
            vfx.Stop();
            firingLoopSound.Stop();
            flameHitbox.enabled = false;
        }
    }
}
