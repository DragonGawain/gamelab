using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefabA; //add prefab in inspector
    [SerializeField] private GameObject playerPrefabB; //add prefab in inspector

    [SerializeField] private NetworkPrefabsList _networkPrefabsList;
    GameObject newPlayer;
    NetworkObject netObj;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            SpawnPlayerServerRpc(0, 1);
            SpawnPlayerServerRpc(1, 0);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            enabled = false;

        var clientId = NetworkManager.Singleton.LocalClientId;
    }

    [ServerRpc(RequireOwnership = false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        _networkPrefabsList.PrefabList.Equals(prefabId);

        if (prefabId == 0)
        {
            newPlayer = Instantiate(playerPrefabA);
        }
        else
        {
            newPlayer = Instantiate(playerPrefabB);
        }

        netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
