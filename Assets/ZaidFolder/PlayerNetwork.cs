using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    

    [SerializeField] private float speed = 1;
    [SerializeField] private Material hostMat;
    [SerializeField] private Renderer playerMesh;
    void Start()
    {
        
        if (OwnerClientId == 0)
        {
            playerMesh.material = hostMat;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            Vector3 velocity = input.normalized * speed;
            transform.Translate(velocity * Time.deltaTime);
        }
        
    }
}
