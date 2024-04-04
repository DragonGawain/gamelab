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
    
    //Void hole spawn time
    private float spawnTime;
    
    //Current level (starts from 1)
    private int levelNumber = 1;
    
    
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
            darkPlayer = FindAnyObjectByType<NetworkDarkPlayer>();
        }

        if (lightPlayer == null)
        {
            lightPlayer = FindAnyObjectByType<NetworkLightPlayer>();
        }

        soTargetManager.lightPlayer = lightPlayer;
        soTargetManager.darkPlayer = darkPlayer;
    }
    
    void spawnVoidHole()
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
            spawnVoidHole();
        }
        
        //Did game reach 4 minutes?
        if (Time.time >= 240 * levelNumber)
        {
            levelNumber++;
        }
    }
    
    private void OnDestroy()
    {
        soTargetManager.ClearTargetManager();
    }
}
