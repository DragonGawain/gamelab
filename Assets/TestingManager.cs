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
        
        UpdateHealth(ServerHealthTMP, serverPlayer.GetHealth());
        //UpdateHealth(ClientHealthTMP, clientPlayer.GetHealth());

        for (int i = 0; i < 4; i++)
        {
            if (dCores[i] != null)
                UpdateHealth(dCoresTMP[i], dCores[i].GetHealth);
            else
            {
                UpdateHealth(dCoresTMP[i], -1);
            }
        }

        if (enemy != null)
        {
            UpdateHealth(EnemyHealthTMP, enemy.health);    
        }
        else
        {
            UpdateHealth(EnemyHealthTMP, -1);
        }
    }
    
    
}
