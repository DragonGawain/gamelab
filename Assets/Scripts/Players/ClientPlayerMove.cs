using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(ServerPlayerMove))] // server component has to exist
    [DefaultExecutionOrder(1)] // executes after server component
    public class ClientPlayerMove : NetworkBehaviour
    {
        [SerializeField]
        ServerPlayerMove serverPlayerMove;
        
        [SerializeField]
        CapsuleCollider capsuleCollider;
        
        [SerializeField]
        PlayerInput playerInput;

        void Awake()
        {
            // disabling collider till we get more info about the entity
            //capsuleCollider.enabled = false;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            enabled = IsClient; // this script is only enabled if we are executing as a client
            
            // object is not the local player object
            if (!IsOwner)
            {
                 enabled = false; // disable this script
                 capsuleCollider.enabled = true;
                 return;
            }
            
            // player input is only enabled on owning players
            playerInput.enabled = true;
        }
            
    }
    
}