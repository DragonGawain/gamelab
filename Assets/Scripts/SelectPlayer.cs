using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SelectPlayer : NetworkBehaviour
{
    private UIManager uiManager;

    //Host Stuff
    [SerializeField]
    private RectTransform hostIcon;

    [SerializeField]
    private RectTransform leftPosition; // Position for Player 1 selection

    [SerializeField]
    private RectTransform middlePosition; // Position for no selection

    [SerializeField]
    private RectTransform rightPosition; // Position for Player 2 selection

    [SerializeField]
    private RectTransform leftArrow;

    [SerializeField]
    private RectTransform rightArrow;

    [SerializeField]
    private RectTransform leftArrowMiddle;

    [SerializeField]
    private RectTransform rightArrowMiddle;

    [SerializeField]
    private RectTransform leftArrowRight;

    [SerializeField]
    private RectTransform rightArrowLeft;

    //Client Stuff
    [SerializeField]
    private RectTransform clientIcon;

    [SerializeField]
    private RectTransform leftPositionClient; // Position for Player 1 selection

    [SerializeField]
    private RectTransform middlePositionClient; // Position for no selection

    [SerializeField]
    private RectTransform rightPositionClient; // Position for Player 2 selection

    [SerializeField]
    private RectTransform leftArrowClient;

    [SerializeField]
    private RectTransform rightArrowClient;

    [SerializeField]
    private RectTransform leftArrowMiddleClient;

    [SerializeField]
    private RectTransform rightArrowMiddleClient;

    [SerializeField]
    private RectTransform leftArrowRightClient;

    [SerializeField]
    private RectTransform rightArrowLeftClient;

    [SerializeField]
    private GameObject darkLight;

    [SerializeField]
    private GameObject lightLight;

    [SerializeField]
    private GameObject darkReady;

    [SerializeField]
    private GameObject lightReady;

    [SerializeField]
    private TextMeshProUGUI x1;

    [SerializeField]
    private TextMeshProUGUI x2;

    private GameObject player1;
    private GameObject player2;
    private GameObject selectedPlayer;

    public static int hostSelection = -1;
    public static bool confirm = false;
    private ulong clientId;

    private bool isInMiddle = true;
    private bool isInMiddleClient = true;
    private bool DarkSelected = false;
    private bool LightSelected = false;
    private bool left = false;
    private bool right = false;
    bool canSelect = true;

    private NetworkVariable<bool> hostConfirmed = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );
    private NetworkVariable<bool> clientConfirmed = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );
    private NetworkVariable<int> hostChoice = new NetworkVariable<int>(
        -1,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    [SerializeField]
    AudioSource confirmAudio;

    Inputs physicalInputs;

    private void Start()
    {
        physicalInputs = GameManager.GetInputActionsAsset();
    }

    public override void OnNetworkSpawn()
    {
        uiManager = FindObjectOfType<UIManager>();

        confirmAudio = GetComponent<AudioSource>();

        // Initialize player GameObjects
        player1 = GameObject.FindGameObjectWithTag("DarkSelect");
        player2 = GameObject.FindGameObjectWithTag("LightSelect");
        x1.enabled = false;
        x2.enabled = false;
        hostIcon.position = middlePosition.position; // Start in the middle
        leftArrow.position = leftArrowMiddle.position; // Start in the middle
        rightArrow.position = rightArrowMiddle.position; // Start in the middle
        clientIcon.position = middlePositionClient.position; // Start in the middle
        leftArrowClient.position = leftArrowMiddleClient.position; // Start in the middle
        rightArrowClient.position = rightArrowMiddleClient.position; // Start in the middle

        lightLight.SetActive(false);
        darkLight.SetActive(false);
        lightReady.SetActive(false);
        darkReady.SetActive(false);

        Debug.Log("All can read:" + hostChoice.CanClientRead(1));

        hostConfirmed.OnValueChanged += (bool previous, bool next) =>
        {
            Debug.Log("Host changed");
        };
        clientConfirmed.OnValueChanged += (bool previous, bool next) =>
        {
            Debug.Log("Client changed");
            uiManager.ShowGameUI();
        };
    }

    public void ResetPositions()
    {
        hostIcon.position = middlePosition.position; // Start in the middle
        leftArrow.position = leftArrowMiddle.position; // Start in the middle
        rightArrow.position = rightArrowMiddle.position; // Start in the middle
        clientIcon.position = middlePositionClient.position; // Start in the middle
        leftArrowClient.position = leftArrowMiddleClient.position; // Start in the middle
        rightArrowClient.position = rightArrowMiddleClient.position; // Start in the middle
        x1.enabled = false;
        x2.enabled = false;
        lightLight.SetActive(false);
        darkLight.SetActive(false);
        lightReady.SetActive(false);
        darkReady.SetActive(false);
    }

    void Update()
    {
        if (physicalInputs == null)
            physicalInputs = GameManager.GetInputActionsAsset();

        if (hostChoice.Value == 0)
        {
            //Debug.Log("host done");
            lightReady.SetActive(true);
        }
        if (hostChoice.Value == 1)
        {
            //Debug.Log("host done");
            darkReady.SetActive(true);
        }

        clientId = NetworkManager.Singleton.LocalClientId;

        if (IsServer && hostConfirmed.Value)
        {
            GetComponent<NetworkObject>().ChangeOwnership(1);
            //GetComponent<NetworkObject>().SpawnWithOwnership(0);
            //GetComponent<NetworkObject>().SpawnWithOwnership(1);
        }

        //Set proper buttons/images
        /*if (NetworkManager.Singleton.LocalClientId == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Move left");
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
                Debug.Log("Move right");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeftClient();
                Debug.Log("client Move left");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRightClient();
                Debug.Log("client Move right");
            }
        }*/

        if (physicalInputs.Player.UISelect.ReadValue<float>() < 0 && canSelect)
        {
            left = true;
            right = false;
            RequestSelectPlayerServerRpc();
        }
        if (physicalInputs.Player.UISelect.ReadValue<float>() > 0 && canSelect)
        {
            right = true;
            left = false;
            RequestSelectPlayerServerRpc();
        }

        clientId = NetworkManager.Singleton.LocalClientId;

        if (IsOwner)
        {
            if (selectedPlayer == player1 && physicalInputs.Player.UIConfirm.triggered)
            {
                x1.enabled = false;

                if (clientId == 1)
                {
                    confirmAudio.Play();
                    clientConfirmed.Value = true;
                    darkReady.SetActive(true);
                    canSelect = false;
                    x1.text = "";
                }
                else
                {
                    confirmAudio.Play();
                    hostSelection = 1;
                    hostChoice.Value = 1;
                    hostConfirmed.Value = true;
                    darkReady.SetActive(true);
                    canSelect = false;
                    x2.text = "";
                }
            }

            if (selectedPlayer == player2 && physicalInputs.Player.UIConfirm.triggered)
            {
                x2.enabled = false;

                if (clientId == 1)
                {
                    confirmAudio.Play();
                    clientConfirmed.Value = true;
                    lightReady.SetActive(true);
                    canSelect = false;
                    x1.text = "";
                }
                else
                {
                    confirmAudio.Play();
                    hostSelection = 0;
                    hostChoice.Value = 0;
                    hostConfirmed.Value = true;
                    lightReady.SetActive(true);
                    canSelect = false;
                    x2.text = "";
                }
            }
        }

        if (hostConfirmed.Value == true && clientConfirmed.Value == true)
        {
            Debug.Log("both players confirmed");
            StartCoroutine(bothSelected());
        }
    }

    IEnumerator bothSelected()
    {
        confirm = true;
        uiManager.ShowGameUI();
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    private void MoveLeft()
    {
        if (isInMiddle && !DarkSelected) // Move to left only if currently in middle
        {
            //Debug.Log("Move left - From middle to left");
            hostIcon.position = leftPosition.position;
            rightArrow.position = rightArrowLeft.position;
            leftArrow.gameObject.SetActive(false);
            darkLight.SetActive(true);

            isInMiddle = false;
            DarkSelected = true;
            Selected(player1);
            selectedPlayer = player1;
            x1.enabled = true;
        }
        else if (hostIcon.position == rightPosition.position) // If on right, move to middle //hostIcon.Value.gameObject.transform.position
        {
            //Debug.Log("Move left - From Right to Middle");
            selectedPlayer = null;
            hostIcon.position = middlePosition.position;
            x1.enabled = false;
            x2.enabled = false;
            isInMiddle = true;
            DarkSelected = false;
            leftArrow.position = leftArrowMiddle.position;
            rightArrow.position = rightArrowMiddle.position;
            leftArrow.gameObject.SetActive(true);
            rightArrow.gameObject.SetActive(true);
            lightReady.SetActive(false);
            darkReady.SetActive(false);
            darkLight.SetActive(false);
            lightLight.SetActive(false);
        }
    }

    private void MoveRight()
    {
        if (isInMiddle && !LightSelected) // Move to right only if currently in middle
        {
            //Debug.Log("Move right - From middle to right");
            hostIcon.position = rightPosition.position;
            leftArrow.position = leftArrowRight.position;
            rightArrow.gameObject.SetActive(false);
            lightLight.SetActive(true);

            isInMiddle = false;
            LightSelected = true;

            Selected(player2);
            selectedPlayer = player2;
            x2.enabled = true;
        }
        else if (hostIcon.position == leftPosition.position) // If on left, move to middle //hostIcon.Value.gameObject.transform.position
        {
            //Debug.Log("Move right - From Left to Middle");

            selectedPlayer = null;
            hostIcon.position = middlePosition.position;
            isInMiddle = true;
            LightSelected = false;
            x1.enabled = false;
            x2.enabled = false;
            leftArrow.position = leftArrowMiddle.position;
            rightArrow.position = rightArrowMiddle.position;
            leftArrow.gameObject.SetActive(true);
            rightArrow.gameObject.SetActive(true);
            lightReady.SetActive(false);
            darkReady.SetActive(false);
            lightLight.SetActive(false);
            darkLight.SetActive(false);
        }
    }

    private void MoveLeftClient()
    {
        if (isInMiddleClient && !DarkSelected) // Move to left only if currently in middle
        {
            clientIcon.position = leftPositionClient.position;
            rightArrowClient.position = rightArrowLeftClient.position;
            leftArrowClient.gameObject.SetActive(false);
            darkLight.SetActive(true);

            isInMiddleClient = false;
            DarkSelected = true;

            Selected(player1);
            selectedPlayer = player1;
            x1.enabled = true;
        }
        else if (clientIcon.position == rightPositionClient.position) // If on right, move to middle // clientIcon.Value.gameObject.transform.position
        {
            //Debug.Log("CLIENT Move left - From Right to Middle");

            selectedPlayer = null;
            //clientIcon.Value.gameObject.transform.position = middlePositionClient.position;
            clientIcon.position = middlePositionClient.position;
            isInMiddleClient = true;
            DarkSelected = false;
            x1.enabled = false;
            x2.enabled = false;
            leftArrowClient.position = leftArrowMiddleClient.position;
            rightArrowClient.position = rightArrowMiddleClient.position;
            leftArrowClient.gameObject.SetActive(true);
            rightArrowClient.gameObject.SetActive(true);
            lightReady.SetActive(false);
            darkReady.SetActive(false);
            darkLight.SetActive(false);
            lightLight.SetActive(false);
        }
    }

    private void MoveRightClient()
    {
        if (isInMiddleClient && !LightSelected) // Move to right only if currently in middle
        {
            clientIcon.position = rightPositionClient.position;
            leftArrowClient.position = leftArrowRightClient.position;
            rightArrowClient.gameObject.SetActive(false);
            lightLight.SetActive(true);

            isInMiddleClient = false;
            LightSelected = true;

            Selected(player2);
            selectedPlayer = player2;
            x2.enabled = true;
        }
        else if (clientIcon.position == leftPositionClient.position) // If on left, move to middle //clientIcon.Value.gameObject.transform.position
        {
            //Debug.Log("CLIENT Move right - From Left to Middle");
            selectedPlayer = null;
            clientIcon.position = middlePositionClient.position;
            isInMiddleClient = true;
            LightSelected = false;
            x1.enabled = false;
            x2.enabled = false;
            leftArrowClient.position = leftArrowMiddleClient.position;
            rightArrowClient.position = rightArrowMiddleClient.position;
            leftArrowClient.gameObject.SetActive(true);
            rightArrowClient.gameObject.SetActive(true);
            lightReady.SetActive(false);
            darkReady.SetActive(false);
            lightLight.SetActive(false);
            darkLight.SetActive(false);
        }
    }

    private void Selected(GameObject selectedPlayer)
    {
        Animator animator = selectedPlayer.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Celebrate");

            Debug.Log(selectedPlayer + " animated");
        }
        else
        {
            Debug.Log("im not animated");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestSelectPlayerServerRpc()
    {
        SelectPlayerClientRpc();
    }

    [ClientRpc]
    void SelectPlayerClientRpc()
    {
        if (clientId == 0)
        {
            if (left)
            {
                MoveLeft();
            }
            if (right)
            {
                MoveRight();
            }
        }
        else
        {
            if (left)
            {
                MoveLeftClient();
            }
            if (right)
            {
                MoveRightClient();
            }
        }
    }
}
