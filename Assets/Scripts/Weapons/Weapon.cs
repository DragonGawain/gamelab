using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    abstract public class Weapon : MonoBehaviour
    {
        [SerializeField] private int damage = 20;
        private string weaponName;
        
        protected void SetDamage(int damage)
        {
            this.damage = damage;
        }

        protected void SetWeaponName(string weaponName)
        {
            this.weaponName = weaponName;
        }

        public int GetDamage()
        {
            return damage;
        }

        public string GetWeaponName()
        {
            return weaponName;
        }

        abstract public void OnFire();
        abstract public void StopFire();

    }
    
    
}

