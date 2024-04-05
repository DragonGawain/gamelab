using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Weapons
{
    abstract public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private int damage = 20;
        private string weaponName;
        protected PlayerTestScript player; // TODO:: edit this to be the networkPlayer (or whatever it's called) instead of the test script when we stop using the test script

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

        public void SetPlayer(PlayerTestScript player)
        {
            this.player = player;
        }

        abstract public void OnFire();
        abstract public void StopFire();
    }
}
