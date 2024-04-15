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
        if (!IsSpawned && !IsServer) return;
        
        UpdateServerHealthClientRpc(serverPlayer.GetHealth());
        UpdateClientHealthClientRpc(clientPlayer.GetHealth());

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
    void UpdateServerHealthClientRpc(int value)
    {
        ServerHealthTMP.text = value.ToString();
    }
    
    [ClientRpc]
    void UpdateClientHealthClientRpc(int value)
    {
        ClientHealthTMP.text = value.ToString();
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
