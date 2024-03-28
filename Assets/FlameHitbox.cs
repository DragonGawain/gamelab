using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class FlameHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnHit(GetComponentInParent<Flamethrower>().GetDamage(), "LightPlayer", GetComponentInParent<Flamethrower>());
        }
    }
}
