using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class DCoreHealthDisplay : NetworkBehaviour
{
    
    [SerializeField] private TextMeshProUGUI[] dCoresTMP;
    
    [SerializeField] private DCore[] dCores;
    // Start is called before the first frame update
    void Update()
    {
        if (!IsSpawned && !IsServer) return;
        
        for (int i = 0; i < 4; i++)
        {
            if (dCores[i] != null)
                UpdateDreamCoreClientRpc(i, dCores[i].GetHealth);
            else
            {
                UpdateDreamCoreClientRpc(i, -1);
            }
        }
    }

    [ClientRpc]
    void UpdateDreamCoreClientRpc(int index, int value)
    {
        
        dCoresTMP[index].text = value.ToString();
    }

    
}
