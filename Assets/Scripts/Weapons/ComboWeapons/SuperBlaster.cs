using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SuperBlaster : Blaster
{
    static int deathTimer = 150;
    
    void Start()
    {
        SetDamage(20);
        SetWeaponName("SuperBlaster");
    }

    public static void ResetSuperHammerTimer()
    {
        deathTimer = 150;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged as "Enemy2"
        if (other.CompareTag("Enemy2"))
        {
            // Attempt to get the Enemy component from the collided object
            Enemy enemy = other.GetComponent<Enemy>();

            // Check if the Enemy component was successfully retrieved
            if (enemy != null)
            {
                // Now that you have a reference to the Enemy component, you can call its OnHit method
                enemy.OnHit(GetDamage(), "LightPlayer", this);
            }
            else
            {
                // Log a message or handle the case where the Enemy component is missing
                Debug.LogWarning("Enemy component not found on object tagged as 'Enemy2'");
            }
        }
    }
}
