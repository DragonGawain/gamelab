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
    private int choice = 0;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            enabled = false;

        var clientId = NetworkManager.Singleton.LocalClientId;
        SpawnPlayerServerRpc(clientId, choice);
        choice++;
    }

    [ServerRpc(RequireOwnership = false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        prefabId = choice;
        _networkPrefabsList.PrefabList.Equals(prefabId);
        GameObject newPlayer;

        if (prefabId == 0)
        {
            newPlayer = Instantiate(playerPrefabA);
        }
        else
        {
            newPlayer = Instantiate(playerPrefabB);
        }

        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
