using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WaveManager : NetworkBehaviour
{
    static int currentWave = 0;
    static Dictionary<int, GameObject> enemyList = new();
    static List<int> randomEnemyOrder = new();
    static List<int> voidIndeces = new();
    static int ET1 = 0;
    static int ET2 = 0;
    static int ET3 = 0;
    static int nbVoid = 0;

    static int totalWaveTime = 0;
    static int nbEnemies = 0;
    static int waveTimer = 0;
    static int nbEnemiesSpawnedThisWave = 0;
    static int nbEnemiesKilledThisWave = 0;

    static GameObject enemyType1;
    static GameObject enemyType2;
    static GameObject enemyType3;

    static List<GameObject> oldVoidHoles = new();
    static List<GameObject> newVoidHoles = new();

    static UIManager uim;

    static bool hasET1Appeared = false;
    static bool hasET2Appeared = false;
    static bool hasET3Appeared = false;

    private static bool gameStart = false;

    void Awake()
    {
        enemyType1 = Resources.Load<GameObject>("EnemyType1");
        enemyType2 = Resources.Load<GameObject>("EnemyType2");
        enemyType3 = Resources.Load<GameObject>("EnemyType3");
        uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        currentWave = 0;
        PlayerSpawner.PlayerSpawn += StartNextWave;
        PlayerSpawner.PlayerSpawn += () => gameStart = true;
    }

    void Start()
    {
        // prime oldVoidHoles with the VOs in the scene
        GameObject[] vos = GameObject.FindGameObjectsWithTag("VoidHole");
        foreach (GameObject vo in vos)
        {
            GameObject voClone = Instantiate(vo);
            newVoidHoles.Add(voClone);
        }
    }

    public static void WaveManagerMasterReset()
    {
        
        currentWave = 0;
        GameObject[] vos = GameObject.FindGameObjectsWithTag("VoidHole");
        foreach (GameObject vo in vos)
        {
            GameObject voClone = Instantiate(vo);
            newVoidHoles.Add(voClone);
        }
        gameStart = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsServer)
        {
            enabled = false;
            return;
        }
    }

    static void StartNextWave()
    {
        //  TODO:: at the start of each wave, show a new wave pop up

        foreach (GameObject oldVo in oldVoidHoles)
        {
            Destroy(oldVo);
        }
        oldVoidHoles = new();
        foreach (GameObject newVo in newVoidHoles)
        {
            newVo.transform.localScale = newVo.transform.localScale * 2f;
            oldVoidHoles.Add(newVo);
        }
        newVoidHoles = new();
        currentWave++;
        uim.ShowWavePopup(currentWave);
        GetWaveInfo(currentWave);
        nbEnemies = ET1 + ET2 + ET3;
        randomEnemyOrder = new();
        voidIndeces = new();
        enemyList = new();
        for (int i = 0; i < nbEnemies; i++)
        {
            randomEnemyOrder.Add(i);
        }
        // We shufle and take the first n entries to be the void enemies.
        // This is to avoid needing to do some funky stuff to ensure that Random.Range gives unique values.
        Shuffle(randomEnemyOrder);
        for (int i = 0; i < nbVoid; i++)
        {
            voidIndeces.Add(randomEnemyOrder[i]);
        }

        for (int i = 0; i < ET1; i++)
        {
            enemyList.Add(i, enemyType1);
        }
        for (int i = ET1; i < ET1 + ET2; i++)
        {
            enemyList.Add(i, enemyType2);
        }
        for (int i = ET1 + ET2; i < ET3 + ET1 + ET2; i++)
        {
            enemyList.Add(i, enemyType3);
        }
        // Shuffle the enemy order again so that the void enemies aren't the first to spawn
        Shuffle(randomEnemyOrder);
    }

    static void GetWaveInfo(int wave)
    {
        waveTimer = 0;
        nbEnemiesKilledThisWave = 0;
        nbEnemiesSpawnedThisWave = 0;
        //uim.ShowWavePopup(wave);
        switch (wave)
        {
            case 1:
                ET1 = 12;
                ET2 = 0;
                ET3 = 0;
                nbVoid = 2;
                totalWaveTime = 3000; // 3000 FUs = 60 seconds = 1 minute
                break;
            case 2:
                ET1 = 7;
                ET2 = 15;
                ET3 = 0;
                nbVoid = 3;
                totalWaveTime = 6000; // 6000 FUs = 2 minutes
                break;
            case 3:
                ET1 = 5;
                ET2 = 10;
                ET3 = 15;
                nbVoid = 5;
                totalWaveTime = 9000; // 9000 FUs = 3 minutes
                break;
            case 4:
                ET1 = 8;
                ET2 = 14;
                ET3 = 14;
                nbVoid = 8;
                totalWaveTime = 12000; // 12000 FUs = 4 minutes
                break;
        }
    }

    // Knuth shuffle algorithm: courtesy of Wikipedia and the unity docs forums
    static void Shuffle(List<int> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            int tmp = items[i];
            int r = Random.Range(i, items.Count);
            items[i] = items[r];
            items[r] = tmp;
        }
    }

    private void FixedUpdate()
    {
        // Good code! This gets the Craig stamp of approval!
        // (except for have squiggly brackets for a 1 statement IF statement)
        //Only runs when players have spawned
        if (!gameStart)
            return;

        waveTimer++;
        if (nbEnemiesSpawnedThisWave < nbEnemies && (waveTimer % (totalWaveTime / nbEnemies)) == 0)
        {
            //  TODO:: CALL THESE - only once (first time the enemy type spawns)
            //UIManager.ShowEnemy1Popup();
            //UIManager.ShowEnemy2Popup();
            //UIManager.ShowEnemy3Popup();



            // instead of instatiating at transform.position, it should be the void hole position
            GameObject enemy = Instantiate(
                enemyList[randomEnemyOrder[nbEnemiesSpawnedThisWave]],
                oldVoidHoles[Random.Range(0, oldVoidHoles.Count)].transform.position + Vector3.up,
                Quaternion.identity
            );

            NetworkObject enemyNetwork = enemy.GetComponent<NetworkObject>();
            enemyNetwork.Spawn();

            if (!hasET1Appeared && enemy.CompareTag("BasicEnemy"))
            {
                hasET1Appeared = true;
                uim.ShowEnemy1Popup();
            }
            if (!hasET2Appeared && enemy.CompareTag("ComboEnemy"))
            {
                hasET2Appeared = true;
                uim.ShowEnemy2Popup();
            }
            if (!hasET3Appeared && enemy.CompareTag("VoidEnemy"))
            {
                hasET3Appeared = true;
                uim.ShowEnemy3Popup();
            }
            if (voidIndeces.Contains(randomEnemyOrder[nbEnemiesSpawnedThisWave]))
                enemy.GetComponent<Enemy>().ThisEnemyIsVoid();

            nbEnemiesSpawnedThisWave++;
        }
    }

    public static void EnemyDied()
    {
        nbEnemiesKilledThisWave++;
        if (nbEnemiesKilledThisWave == nbEnemies)
        {
            if (currentWave == 4)
            {
                // TODO: actually implement this
                GameManager.SetYouWin();
            }
            else
                StartNextWave();
        }
    }

    public static void VoidEnemyDied(GameObject vo)
    {
        newVoidHoles.Add(vo);
    }
}
