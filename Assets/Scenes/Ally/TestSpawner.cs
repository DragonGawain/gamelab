using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using static ComboHitBox;
using static ComboAttackManager;


public class TestSpawner : MonoBehaviour
{ 
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform enemyPrefab2;
    [SerializeField] private Transform enemyPrefab3;
    //[SerializeField] private Transform enemyPrefab4;
    //[SerializeField] private Transform enemyPrefab5;
    [SerializeField] WeaponType weaponType;
    public LevelManager level;
    [SerializeField] private int health = 100;



    private void Start()
    {
   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnEnemy(enemyPrefab);
            Debug.Log("enemy tag: " + enemyPrefab.tag);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnEnemy(enemyPrefab2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnEnemy(enemyPrefab3);
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SpawnEnemy(enemyPrefab4);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    SpawnEnemy(enemyPrefab5);
        //}
    }

    void SpawnEnemy(Transform enemyPrefab)
    {
        // Instantiate the enemy directly above the spawn point
        Instantiate(enemyPrefab, transform.position + Vector3.up, Quaternion.identity);
    }


    private void HitByBlaster(Blaster blaster)
    {
        // deals blaster damage to health
        health -= blaster.GetDamage();

        // any other blaster effects can go here

    }

    private void HitByHammer(Hammer hammer)
    {
        // deals blaster damage to health
        health -= hammer.GetDamage();

        // any other blaster effects can go here

    }

    private void HitByFlamethrower(Flamethrower thrower)
    {
        // deals blaster damage to health
        health -= thrower.GetDamage();

        // any other blaster effects can go here

    }

    private void HitByGrenadeLauncher(GrenadeLauncher launcher)
    {
        // deals blaster damage to health
        health -= launcher.GetDamage();

        // any other blaster effects can go here

    }

 


}
