using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int damage = 20;
        private string weaponName;
        
        public void SetDamage(int damage)
        {
            this.damage = damage;
        }

        public void SetWeaponName(string weaponName)
        {
            this.weaponName = weaponName;
        }
        
    }
    
    
}

