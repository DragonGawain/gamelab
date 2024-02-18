using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button server_button;
    [SerializeField] private Button host_button;
    [SerializeField] private Button client_button;

    private void Awake()
    {
        server_button.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        host_button.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        client_button.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
