using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ComboHitBox;
using static ComboAttackManager;
using static Enemy;
using Weapons;


public class Enemy1 : Enemy
{

    [SerializeField] WeaponType weaponType;
    private string weaponName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void dealDamage(Weapon weapon)
    {
        string weaponName = weapon.GetWeaponName();
        int dmg = weapon.GetDamage();

        // Use weaponName to determine the specific weapon and call the appropriate method
        if (weaponName == "Blaster")
        {
            health -= weapon.GetDamage();

            //HitByBlaster(weapon as Blaster); // Cast to Blaster if you're sure it's a Blaster
        }
        else if (weaponName == "Flamethrower")
        {
            health -= weapon.GetDamage();

            //HitByFlamethrower(weapon as Flamethrower);
        }
        else if (weaponName == "Hammer")
        {
            health -= weapon.GetDamage();

            //HitByHammer(weapon as Hammer);
        }
        else if (weaponName == "GrenadeLauncher")
        {
            health -= weapon.GetDamage();

            //HitByGrenadeLauncher(weapon as GrenadeLauncher);
        }


        Debug.Log(weapon.GetWeaponName() + "doing damage: " + dmg);
    }


    private void HitByBlaster(Blaster blaster)
    {
        // deals blaster damage to health
        health -= blaster.GetDamage();
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        // any other blaster effects can go here

    }

    private void HitByHammer(Hammer hammer)
    {
        // deals blaster damage to health
        health -= hammer.GetDamage();
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        // any other blaster effects can go here

    }

    private void HitByFlamethrower(Flamethrower thrower)
    {
        // deals blaster damage to health
        health -= thrower.GetDamage();
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        // any other blaster effects can go here

    }

    private void HitByGrenadeLauncher(GrenadeLauncher launcher)
    {
        // deals blaster damage to health
        health -= launcher.GetDamage();
        Debug.Log("grensde dmg:" + launcher.GetDamage());
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        // any other blaster effects can go here

    }

    private void OnDeath()
    {
        // Put some animation player also
        Destroy(this.gameObject);
    }
}