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

        Debug.Log("ClientId: " + clientId);

        SpawnPlayerServerRpc(clientId, choice);
        choice++;
    }

    [ServerRpc(RequireOwnership = false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        Debug.Log(choice);
        prefabId = choice;
        _networkPrefabsList.PrefabList.Equals(prefabId);
        GameObject newPlayer;

        if (prefabId == 0)
        {
            //choice++;
            Debug.Log("light player");
            newPlayer = Instantiate(playerPrefabA);
        }
        else
        {
            Debug.Log("dark player");
            newPlayer = Instantiate(playerPrefabB);
        }

        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
