using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

public class Bullet : MonoBehaviour
{
    readonly int dmg = 10;

    public void Explode()
    {
        // put an animation player here or smth instead of the bullet just disappearing
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasicEnemy"))
            other.GetComponent<Enemy>().OnHit(dmg, "LightPlayer");
    }
}
