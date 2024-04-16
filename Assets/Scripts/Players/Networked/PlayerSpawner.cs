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

    private int hostOption;
    private int clientOption;

    public static Action PlayerSpawn;

    public TestingManager TM;

    public void Update()
    {
        
        // REMOVE COMMENTS AFTER UI to GAME SCENE IS FIXED, Done
         hostOption = SelectPlayer.hostSelection;
         
         if (hostOption == 0) { clientOption = 1; }
         else { clientOption = 0; }
         
         if(SelectPlayer.confirm)
         {
             Debug.Log("Spawning players...");
             SpawnPlayerServerRpc(0, hostOption);
             SpawnPlayerServerRpc(1, clientOption);
             SelectPlayer.confirm = false;
         }
         

        //For Zaid to test weapon stuff
        //DELETE CODE AFTER UI IS FIXED
        /*if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnPlayerServerRpc(0, 1);
            SpawnPlayerServerRpc(1, 0);
            PlayerSpawn();
            if (TM!=null){TM.enabled = true;}
            
        }*/
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
        
        
        /*if (clientId == 0 && TM != null)
        {
            TM.serverPlayer = newPlayer.GetComponent<PlayerTestScript>();
        }
        else if (clientId == 1 && TM != null)
        {
            TM.clientPlayer = newPlayer.GetComponent<PlayerTestScript>();
        }*/

        netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}