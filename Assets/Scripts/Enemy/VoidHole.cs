// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using Random = UnityEngine.Random;


// public class VoidHole : MonoBehaviour
// {
//     public static event Action<Transform> DespawnVoidHole;
//     [SerializeField] private SO_TargetManager soTargetManager;
    
//     [SerializeField] private Transform basicEnemyPrefab;
//     [SerializeField] private Transform comboEnemyPrefab;
//     [SerializeField] private Transform voidEnemyPrefab;

//     public LevelManager level;

//     //Info updated *with* LevelManager 
//     private float spawnTime;
//     private int enemyCount;
    
//     private void Start()
//     {
//         enemyCount = level.enemyCount + Random.Range(0, level.enemyRange);
//         spawnTime = level.voidHolestartTime + Time.time;
//         print("ENEMY COUNT: " + enemyCount);
//     }

//     void Update()
//     {
//         if (Time.time > spawnTime)
//         {
//             spawnTime = Time.time + level.enemySpawnTime + Random.Range(0, 10);
//             SpawnEnemy();
//         }
//     }

    

//     private void SpawnEnemy()
//     {
//         //CHANGE TO BASIC
//         EnemyAI enemyAiRef = EnemyAI.Create(basicEnemyPrefab, transform.position);


//         if (--enemyCount <= 0)
//         {
//             DestroySelf();
//         }
//     }
    
//     private void DestroySelf()
//     {
//         DespawnVoidHole?.Invoke(transform.parent);
//         Destroy(this.gameObject);
//     }
// }
