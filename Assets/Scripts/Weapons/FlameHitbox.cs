using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class FlameHitbox : MonoBehaviour
{
    private float tick = 0.5f;
    private float shootTime = 0;
    readonly int dmg = 15;

    List<Enemy> enemies = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasicEnemy"))
            enemies.Add(other.GetComponent<Enemy>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BasicEnemy") && enemies.Contains(other.GetComponent<Enemy>()))
            enemies.Remove(other.GetComponent<Enemy>());
    }

    private void FixedUpdate()
    {
        if (Time.time < shootTime)
            return;
        shootTime = Time.time + tick;
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.OnHit(dmg, "LightPlayer");
            }
            else
            {
                enemies.Remove(enemy);
            }
        }
    }

}
