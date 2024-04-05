using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class SuperBlaster : Blaster
{
    static int deathTimer = 450;
    
    void Start()
    {
        SetDamage(20);
        SetWeaponName("SuperBlaster");
        // This is not a 'safe' way, but it will work. Just make sure that the fire point is alwayse the 0th child. 
        firePoint = transform.GetChild(0); 
    }

    
    void FixedUpdate()
    {
        deathTimer--;
        if (deathTimer <= 0)
        {
            // instance = null;
            player.SetIsBlasterSuper(false);
            // Destroy(this.gameObject);
        }
    }

    
    public static void ResetSuperBlasterTimer()
    {
        deathTimer = 450;
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     // Check if the collided object is tagged as "Enemy2"
    //     if (other.CompareTag("ComboEnemy"))
    //     {
    //         // Attempt to get the Enemy component from the collided object
    //         Enemy enemy = other.GetComponent<Enemy>();

    //         // Check if the Enemy component was successfully retrieved
    //         if (enemy != null)
    //         {
    //             // Now that you have a reference to the Enemy component, you can call its OnHit method
    //             enemy.OnHit(GetDamage(), "LightPlayer", this);
    //         }
    //         else
    //         {
    //             // Log a message or handle the case where the Enemy component is missing
    //             Debug.LogWarning("Enemy component not found on object tagged as 'Enemy2'");
    //         }
    //     }
    // }
}
