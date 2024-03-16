// using System.Collections;
// using System.Collections.Generic;
// using Unity.AI.Navigation;
// using UnityEngine;
//
// public class EnemySpawner : MonoBehaviour
// {
//     // reference for enemy which is going to be spawned
//     [SerializeField] private Transform enemyPrefab;
//
//     [Header("Spawn Locations")]
//     [SerializeField] private Transform[] outsideSpawnPointsArray;
//
//     // soTargetManager is very important here.
//     // it gives suitable targets for enemies
//     [SerializeField] private SO_TargetManager soTargetManager;
//
//     // spawned enemies will be stored here
//     private List<Transform> spawnedEnemyList = new List<Transform>();
//
//     // when enemies destroy a wall, NavMesh system have to be updated
//     // to do that, i gave this reference, to re-bake it
//     [SerializeField] private NavMeshSurface navmeshSurface;
//
//     // created the enums to use them when decide where to spawn enemies
//     // (not spawning inside yet)
//     private enum SpawnLocation
//     {
//         Outside,
//         Inside
//     }
//
//     private void Awake()
//     {
//         // we give navmesh info to the soTargetManager
//         soTargetManager.navmeshSurface = navmeshSurface;
//     }
//
//     private void SpawnEnemy(SpawnLocation spawnLocation)
//     {
//         Vector3 spawnPos = Vector3.zero; // initial pos
//
//         // where we spawn the enemies? outside or inside
//         switch (spawnLocation)
//         {
//             case SpawnLocation.Outside:
//                 spawnPos = GetOutsideSpawnPosition();
//                 break;
//             case SpawnLocation.Inside:
//                 // logic for spawning inside not yet implemented            
//                 return;
//         }
//         
//         // we create the enemy and add it into the enemy list
//         EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab, spawnPos);
//         spawnedEnemyList.Add(enemyAiRef.transform);
//     }
//     private void Update()
//     {
//         // this is for testing purposes
//         // the important thing is that you need to call
//         // soTargetManager.IsThereEmptyWallSlot() method, and
//         // you should spawn enemy when it returns true
//         if (Input.GetKeyDown(KeyCode.S) && soTargetManager.IsThereEmptyWallSlot()) SpawnEnemy(SpawnLocation.Outside);
//     }
//
//     private Vector3 GetOutsideSpawnPosition()
//     {
//         // returning a position randomly selected between outer spawn points
//         int index = Random.Range(0, outsideSpawnPointsArray.Length);
//         return outsideSpawnPointsArray[index].position;
//     }
//
//     private void OnDestroy()
//     {
//         // THIS IS VERY IMPORTANT,
//         // because I used scriptable objects for soTargetManager, it should
//         // be cleared when stopping the game, otherwise it will give errors
//         soTargetManager.ClearTargetManager();
//     }
//
//
//
//
//
//
//
//
// }
