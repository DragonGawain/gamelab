using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class VoidHole : MonoBehaviour
{
    public static event Action<Transform> DespawnVoidHole;
    [SerializeField] private SO_TargetManager soTargetManager;
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform enemyPrefab2;
    [SerializeField] private Transform enemyPrefab3;
    [SerializeField] private Transform enemyPrefab4;
    [SerializeField] private Transform enemyPrefab5;


    public LevelManager level;

    private float spawnTime;
    
    

    private int enemyCount;
    private void Start()
    {
        enemyCount = level.enemyCount + Random.Range(0, level.enemyRange);
        //print("ENEMY COUNT: " + enemyCount);
    }

    void Update()
    {
        //if (Time.time > spawnTime)
        //{
        //    spawnTime = Time.time + level.enemySpawnTime + Random.Range(0, 10);
        //    SpawnRandomEnemy();
        //}

       // Example of a conditional enemy spawn based on a specific input and condition
         if (Input.GetKeyDown(KeyCode.Z) && soTargetManager.IsThereDCore())
        {
            SpawnRandomEnemy();
        }
    }

    void SpawnRandomEnemy()
    {
        int enemyType = Random.Range(1, 5); // Generates a random number between 1 and 4, each representing an enemy type

        switch (enemyType)
        {
            case 1:
                SpawnEnemy();
                break;
            case 2:
                SpawnEnemy2();
                break;
            case 3:
                SpawnEnemy3();
                break;
            case 4:
                SpawnEnemy4();
                break;
            case 5:
                SpawnEnemy5();
                break;
            default:
                Debug.LogError("Invalid enemy type selected");
                break;
        }
    }

    private void SpawnEnemy()
    {
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab, transform.position);


        if (--enemyCount <= 0)
        {
            DestroySelf();
        }
    }

    private void SpawnEnemy2()
    {
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab2, transform.position);


        if (--enemyCount <= 0)
        {
            DestroySelf();
        }
    }

    private void SpawnEnemy3()
    {
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab3, transform.position);


        if (--enemyCount <= 0)
        {
            DestroySelf();
        }
    }

    private void SpawnEnemy4()
    {
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab4, transform.position);


        if (--enemyCount <= 0)
        {
            DestroySelf();
        }
    }


    private void SpawnEnemy5()
    {
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab5, transform.position);


        if (--enemyCount <= 0)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        DespawnVoidHole?.Invoke(transform.parent);
        Destroy(this.gameObject);
    }
}
