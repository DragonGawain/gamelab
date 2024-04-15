using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TestingManager : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI ServerHealthTMP;
    [SerializeField] private TextMeshProUGUI ClientHealthTMP;
    [SerializeField] private TextMeshProUGUI EnemyHealthTMP;
    
    [SerializeField] private TextMeshProUGUI[] dCoresTMP;


    public PlayerTestScript serverPlayer;
    public PlayerTestScript clientPlayer;

    [SerializeField] private DCore[] dCores;
    
    public static Enemy enemy;
    

    public void UpdateHealth(TextMeshProUGUI TMP, int health)
    {
        TMP.text = health.ToString();
    }

    public void Update()
    {
        if (!IsSpawned) return;
        
        UpdateServerHealthClientRpc();
        UpdateClientHealthClientRpc();

        for (int i = 0; i < 4; i++)
        {
            if (dCores[i] != null)
                UpdateDreamCoreClientRpc(i, dCores[i].GetHealth);
            else
            {
                UpdateDreamCoreClientRpc(i, -1);
            }
        }

        if (enemy != null)
        {
            UpdateEnemyHealthClientRpc(enemy.health);
        }
        else
        {
            UpdateEnemyHealthClientRpc(-1);
        }
    }

    [ClientRpc]
    void UpdateServerHealthClientRpc()
    {
        ServerHealthTMP.text = serverPlayer.GetHealth().ToString();
    }
    
    [ClientRpc]
    void UpdateClientHealthClientRpc()
    {
        ClientHealthTMP.text = clientPlayer.GetHealth().ToString();
    }

    [ClientRpc]
    void UpdateDreamCoreClientRpc(int index, int value)
    {
        
        dCoresTMP[index].text = value.ToString();
    }

    [ClientRpc]
    void UpdateEnemyHealthClientRpc(int value)
    {
        EnemyHealthTMP.text = value.ToString();
    }
}
