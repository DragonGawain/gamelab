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
    bool semaphore = false;

    private void OnTriggerEnter(Collider other)
    {
        semaphore = true;
        if (other.CompareTag("BasicEnemy"))
            enemies.Add(other.GetComponent<Enemy>());
        semaphore = false;
    }

    private void OnTriggerExit(Collider other)
    {
        semaphore = true;
        if (other.CompareTag("BasicEnemy") && enemies.Contains(other.GetComponent<Enemy>()))
            enemies.Remove(other.GetComponent<Enemy>());
        semaphore = false;
    }

    private void FixedUpdate()
    {
        if (Time.time < shootTime)
            return;
        if (!semaphore)
        {
            shootTime = Time.time + tick;
            foreach (Enemy enemy in enemies)
            {
                if (semaphore)
                    return;
                if (enemy != null)
                    enemy.OnHit(dmg, "LightPlayer");
                else
                    enemies.Remove(enemy);
            }
        }
    }
}
