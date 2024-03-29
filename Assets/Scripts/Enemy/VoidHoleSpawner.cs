using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class VoidHoleSpawner : MonoBehaviour
{
    //List of spawn points that are children of spawner
    private List<Transform> spawnPoints;
    //Prefab of void hole
    [SerializeField] private VoidHole voidHolePrefab;
    
    [SerializeField] private SO_TargetManager soTargetManager;

    [SerializeField] private PlayerTestScript darkPlayer;
    [SerializeField] private PlayerTestScript lightPlayer;

    [SerializeField] private LevelManager level;

    private float spawnTime;
    void Start()
    {
        spawnTime = level.startTime;
        
        //Get all spawners and remove itself from list
        spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
        
        spawnPoints.Remove(transform);

        //Add spawn point back to list after being available again 
        VoidHole.DespawnVoidHole += addSpawner;

        if (darkPlayer == null)
        {
            darkPlayer = FindAnyObjectByType<DarkPlayer>();
        }

        if (lightPlayer == null)
        {
            lightPlayer = FindAnyObjectByType<LightPlayer>();
        }

        soTargetManager.lightPlayer = lightPlayer;
        soTargetManager.darkPlayer = darkPlayer;
    }
    
    void spawn()
    {
        //Find a random spawner and creat void hole, then remove spawner from available spawners
        if (spawnPoints.Count == 0)
        {
            spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
        }
        
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        VoidHole voidHole = Instantiate(voidHolePrefab, spawnPoints[spawnIndex]);
        voidHole.level = level;
        
        spawnPoints.RemoveAt(spawnIndex);

    }

    public void addSpawner(Transform spawner)
    {
        
        print("ADDING SPAWNER " + spawner.name);
        spawnPoints.Add(spawner);
    }

    
    private void Update()
    {

        if (Time.time > spawnTime)
        {
            spawnTime = Time.time + level.voidHoleSpawnTime + Random.Range(0, level.voidHoleSpawnRange); 
            spawn();
        }
        // if (Input.GetKeyDown(KeyCode.X) && spawnPoints.Count > 0)
        // {
        //     spawn();
        // }
    }
    
    private void OnDestroy()
    {
        soTargetManager.ClearTargetManager();
    }
}
