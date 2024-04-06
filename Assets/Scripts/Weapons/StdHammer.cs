using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class StdHammer : Hammer
    {
        private AudioSource soundEffect;
        private void Start()
        {
            dmg = 20;
            SetWeaponName("Hammer");
            soundEffect = GetComponent<AudioSource>();
        }

        protected override void slam()
        {
            base.slam();
            
            // play sound
            soundEffect.Play();
            
        }
    }
}
