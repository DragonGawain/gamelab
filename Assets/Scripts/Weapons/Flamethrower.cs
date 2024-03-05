using System.Collections;
using System.Collections.Generic;
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

        // Update is called once per frame
        void Update()
        {
            
        }

        // TODO: make the fire particles stop playing after no longer pressed etc        
        public void Fire()
        {
            Debug.Log("pew pew!");
            particles.Play();
            
        }
    }
}

