using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

public class Bullet : MonoBehaviour
{
    private Weapon weaponRef; // passed from the weapon its firing from

    public void SetWeaponRef(Weapon weapon)
    {
        weaponRef = weapon;
    }

    public Weapon GetWeaponRef()
    {
        return weaponRef;
    }

    public void Explode()
    {
        // put an animation player here or smth instead of the bullet just disappearing
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnHit(weaponRef.GetDamage(), "LightPlayer");
        }
    }
}
