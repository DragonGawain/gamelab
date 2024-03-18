using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidHoleSpawner : MonoBehaviour
{
    //List of spawn points that are children of spawner
    private List<Transform> spawnPoints;
    //Prefab of void hole
    [SerializeField] private VoidHole voidHolePrefab;
    
    [SerializeField] private SO_TargetManager soTargetManager;
    void Start()
    {
        //Get all spawners and remove itself from list
        spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
        spawnPoints.Remove(transform);

        //Add spawn point back to list after being available again 
        VoidHole.DespawnVoidHole += addSpawner;
    }

    void spawn()
    {
        //Find a random spawner and creat void hole, then remove spawner from available spawners
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        Instantiate(voidHolePrefab, spawnPoints[spawnIndex]);
        spawnPoints.RemoveAt(spawnIndex);

    }

    private void addSpawner(Transform spawner)
    {
        spawnPoints.Add(spawner);
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X) && spawnPoints.Count > 0)
        {
            spawn();
        }
    }
    
    private void OnDestroy()
    {
        soTargetManager.ClearTargetManager();
    }
}
