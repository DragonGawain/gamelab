using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class FlameHitbox : MonoBehaviour
{

    private float tick = 0.5f;

    private float shootTime = 0;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BasicEnemy"))
        {
            if (Time.time < shootTime)
                return;
            shootTime = Time.time + tick;
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnHit(GetComponentInParent<Flamethrower>().GetDamage(), "LightPlayer", GetComponentInParent<Flamethrower>());
        }
    }
}
