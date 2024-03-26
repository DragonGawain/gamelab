using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManager", menuName = "LevelManager/New Level")]
public class LevelManager : ScriptableObject
{

    public float startTime;
    
    public float voidHoleSpawnTime;
    public float enemySpawnTime;


    public float voidHoleSpawnRange;
    public float enemySpawnRange;

    public int enemyCount;
    public int enemyRange;

}
