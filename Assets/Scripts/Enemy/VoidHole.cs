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
    private List<Transform> spawnedEnemyList = new List<Transform>();
    
    

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z) && soTargetManager.IsThereDCore())
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab, transform.position);
        spawnedEnemyList.Add(enemyAiRef.transform);
    }

    private void DestroySelf()
    {
        DespawnVoidHole?.Invoke(transform.parent);
        soTargetManager.ClearTargetManager();
        Destroy(this.gameObject);
    }
}
