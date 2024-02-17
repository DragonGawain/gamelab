using Unity.Netcode;
using UnityEngine;

namespace Players
{
    [DefaultExecutionOrder(0)] //to execute before the client component
    public class ServerPlayerMove : NetworkBehaviour
    {
        // this represents the player's networked position
        [SerializeField]
        Vector3 localHeldPosition;

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                 enabled = false; // if its a client, don't enable this script
                 return;
            }
            
            // otherwise, it must be a server:
            OnServerSpawnPlayer();
            base.OnNetworkSpawn();
        }

        void OnServerSpawnPlayer()
        {
            // get next spawn point
            var spawnPoint = ServerPlayerSpawnPoints.Instance.ConsumeNextSpawnPoint();
            
            // if spawn point exists, set spawnPosition to the spawnPoint position, otherwise set to 0
            var spawnPosition = spawnPoint ? spawnPoint.transform.position : Vector3.zero;
            
            // set player position to spawn position
            transform.position = spawnPosition;

        }
        
    }
}