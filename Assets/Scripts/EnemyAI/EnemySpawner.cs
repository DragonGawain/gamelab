using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;

    [Header("Spawn Locations")]
    [SerializeField] private SpawnSlotCouple[] outsideSpawnSlotCouples;
    [SerializeField] private Transform[] insideSpawnTransforms;
    [SerializeField] private BedroomDoorSlot bedroomDoorSlot;


    private List<Transform> spawnedEnemyList = new List<Transform>();

    private enum SpawnLocation
    {
        Outside,
        Inside
    }

    [System.Serializable] private struct SpawnSlotCouple
    {
        public Transform spawnTransform;
        public WallSlot slot;
    }

    private void SpawnEnemy(SpawnLocation spawnLocation)
    {
        SpawnSlotCouple _spawnSlotCouple = default;
        switch (spawnLocation)
        {
            case SpawnLocation.Outside:
                _spawnSlotCouple = GetRandomSpawnSlotCouple(outsideSpawnSlotCouples);
                break;
            case SpawnLocation.Inside:
                _spawnSlotCouple = GetRandomSpawnSlotCouple(outsideSpawnSlotCouples);
                break;
        }
        if (_spawnSlotCouple.spawnTransform == null)
        {
            Debug.Log("no empty slot!");
            return;
        }
        EnemyAI enemyAiRef = EnemyAI.Create(enemyPrefab, _spawnSlotCouple.spawnTransform.position, _spawnSlotCouple.slot, bedroomDoorSlot);
        _spawnSlotCouple.slot.isFilled = true;
        
        spawnedEnemyList.Add(enemyAiRef.transform);
    }

    private SpawnSlotCouple GetRandomSpawnSlotCouple(SpawnSlotCouple[] spawnSlotCouples)
    {
        int index = Random.Range(0, spawnSlotCouples.Length);
        if (spawnSlotCouples[index].slot.isFilled == false) return spawnSlotCouples[index];
        else return default;
    }

    private void Start()
    {
        Invoke(nameof(CheckForSpawn), 1f);
    }

    private void CheckForSpawn()
    {
        SpawnSlotCouple spawnSlotCouple = GetRandomSpawnSlotCouple(outsideSpawnSlotCouples);
        if (spawnSlotCouple.slot != null && spawnSlotCouple.slot.isFilled == false)
        {
            SpawnEnemy(SpawnLocation.Outside);
        }

        Invoke(nameof(CheckForSpawn), 1f);
    }


}
