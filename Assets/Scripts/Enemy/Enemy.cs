using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Weapons;
using Sequence = DG.Tweening.Sequence;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private int damage;
    [SerializeField] private Material material;

    private void OnTriggerEnter(Collider other)
    {
        // Been hit by blaster
        
        if (other.gameObject.name == "Bullet(Clone)")
        {
         
            Bullet bullet = other.gameObject.GetComponentInParent<Bullet>();
            
            // TODO: play bullet exploding animation?
            bullet.Explode(); 
            
            OnHit();
            
            // If the bullet came from a blaster gun
            if (bullet.GetWeaponRef() as Blaster)
                HitByBlaster(bullet.GetWeaponRef() as Blaster);
            
            // If the bullet came from another gun later..etc.
            // else if..
                
            Destroy(other.gameObject);
            
        }
        
        
        
    }
    private void OnHit()
    {
        Debug.Log(health);
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        
        // Play hit animation (e.g. enemy gets stunned or smth)
            
        
        // Enemy flashes red then turns back to normal
        Sequence sequence = DOTween.Sequence();
        sequence.Append(material.DOColor(Color.red, 0.2f));
        sequence.Append(material.DOColor(Color.white, 0.2f));
        sequence.Play();
    }

    private void HitByBlaster(Blaster blaster)
    {
        // deals blaster damage to health
        health -= blaster.GetDamage();
        
        // any other blaster effects can go here

    }

    private void OnDeath()
    {
        // Put some animation player also
        Destroy(this.gameObject);
    }
    
}
