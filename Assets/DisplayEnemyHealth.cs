using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;


public class DisplayEnemyHealth : NetworkBehaviour
{
    // Start is called before the first frame update

    private Enemy enemy;
    
    private TextMeshProUGUI TMP;
    

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsServer)
        {
            enabled = false;
            return;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            enemy = FindObjectOfType<Enemy>();
        }
        else
        {
            UpdateTextClientRpc(enemy.health);
        }
    }

    [ClientRpc]
    void UpdateTextClientRpc(int health)
    {
        TMP = GetComponent<TextMeshProUGUI>();   
        TMP.text =  health.ToString();
    }
}
