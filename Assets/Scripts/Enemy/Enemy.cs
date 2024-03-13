using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Weapons;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        // Been hit by blaster
        
        if (other.gameObject.name == "Bullet(Clone)")
        {
            Bullet bullet = other.gameObject.GetComponentInParent<Bullet>();
            
            // TODO: play bullet exploding animation?
            bullet.Explode(); 
            
            // If the bullet came from a blaster gun
            if (bullet.GetWeaponRef() as Blaster)
                HitByBlaster(bullet.GetWeaponRef() as Blaster);
            
            // If the bullet came from another gun later..etc.
            // else if..
                
            Destroy(bullet);
            
        }
        
    }
    private void OnHit()
    {
        // should play a hit animation, enemy flashes red then turns back to normal
        
    }

    private void HitByBlaster(Blaster blaster)
    {
        // deals blaster damage to health
        health -= blaster.GetDamage();
        
        // any other blaster effects can go here

    }
    
}
