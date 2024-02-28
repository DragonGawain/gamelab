using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;

    [Header("Spawn Locations")]
    [SerializeField] private Transform[] outsideSpawnPointsArray;

    [SerializeField] private SO_TargetManager soTargetManager;


    private List<Transform> spawnedEnemyList = new List<Transform>();

    [SerializeField] private NavMeshSurface navmeshSurface;

    private enum SpawnLocation
    {
        Outside,
        Inside
    }

    private void Awake()
    {
        soTargetManager.navmeshSurface = navmeshSurface;
    }
    private void SpawnEnemy(SpawnLocation spawnLocation)
    {
        Vector3 spawnPos = Vector3.zero;
        // where we spawn the enemies? outside or inside
        switch (spawnLocation)
        {
            case SpawnLocation.Outside:
                //Debug.Log("spawning outside...");
                spawnPos = GetOutsideSpawnPosition();
                break;
            case SpawnLocation.Inside:
                //Debug.Log("can not spawning inside");              
                return;
        }

        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab, spawnPos);
        spawnedEnemyList.Add(enemyAiRef.transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && soTargetManager.IsThereEmptyWallSlot()) SpawnEnemy(SpawnLocation.Outside);
    }

    private Vector3 GetOutsideSpawnPosition()
    {
        // returning a position randomly selected between outer spawn points
        int index = Random.Range(0, outsideSpawnPointsArray.Length);
        return outsideSpawnPointsArray[index].position;
    }

    private void OnDestroy()
    {
        soTargetManager.ClearTargetManager();
    }








}
