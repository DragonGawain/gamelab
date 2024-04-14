using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Weapons;

namespace Weapons
{
    public class StdHammer : Hammer
    {
        private AudioSource soundEffect;
        [SerializeField] private VisualEffect vfx;
        private void Start()
        {
            dmg = 25;
            SetWeaponName("Hammer");
            soundEffect = GetComponent<AudioSource>();
        }

        protected override void slam()
        {
            base.slam();
            
            // play sound
            soundEffect.Play();
            
            // play VFX
            vfx.Play();      
            
        }
    }
}
