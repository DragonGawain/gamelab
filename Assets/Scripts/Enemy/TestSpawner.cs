using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using static ComboAttackManager;


public class TestSpawner : MonoBehaviour
{ 
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform enemyPrefab2;
    [SerializeField] private Transform enemyPrefab3;
   


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


 


}
