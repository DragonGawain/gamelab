using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager
{
    public int chosenCharacter = 0;
    [SerializeField] GameObject[] characters;
    GameObject player;

    void Awake()
    {
        chosenCharacter = PlayerTestScript.selection;

        if(IsServer)
        {
            player = Instantiate(characters[0], new Vector3(-5f, 0f,0f), Quaternion.identity) as GameObject;

        }
        else
        {
            player = Instantiate(characters[1], new Vector3(-2f, 0f, 0f), Quaternion.identity) as GameObject;
        }
    }
}