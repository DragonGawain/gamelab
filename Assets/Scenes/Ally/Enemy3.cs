using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ComboAttackManager;
using static Enemy;
using Weapons;


public class Enemy3 : Enemy
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //grenade launcher and bullet blaster
    public override void OnHit(int dmg, string playerTag, Weapon weapon)
    {

        string weaponName = weapon.GetWeaponName();
        dmg = weapon.GetDamage();
        Debug.Log("WEAPON: " +weapon.GetWeaponName());

        if (weaponName == "BulletBarrage")
        {
            health -= weapon.GetDamage();

        }

        if (weaponName == "SuperBullet")
        {
            health -= weapon.GetDamage();

        }

        Debug.Log(" enemy health: " + health);
        health -= weapon.GetDamage();
        Debug.Log("weapon get damage: " + weapon.GetDamage());

        Debug.Log(weapon.GetWeaponName() + "doing damage: " + dmg);

        if (health <= 0)
        {
            OnDeath();

        }
    }
      private void OnDeath()
    {
        // Put some animation player also
        Destroy(this.gameObject);
    }
 
}