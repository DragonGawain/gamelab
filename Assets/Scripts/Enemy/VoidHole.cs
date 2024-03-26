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

    public LevelManager level;

    private float spawnTime;
    
    

    private int enemyCount;
    private void Start()
    {
        enemyCount = level.enemyCount + Random.Range(0, level.enemyRange);
        print("ENEMY COUNT: " + enemyCount);
    }


    void Update()
    {
        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + level.enemySpawnTime + Random.Range(0, level.enemySpawnRange);;
            SpawnEnemy();
        }
        
        // if (Input.GetKeyDown(KeyCode.Z) && soTargetManager.IsThereDCore())
        // {
        //     SpawnEnemy();
        // }
    }
    private void SpawnEnemy()
    {
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab, transform.position);
        

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
