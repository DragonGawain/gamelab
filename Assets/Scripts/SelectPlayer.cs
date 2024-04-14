using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPlayer : NetworkBehaviour
{
    private UIManager uiManager;

    //Host Stuff
    [SerializeField]
    private RectTransform hostIcon; // The transform of the selection icon

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
    private RectTransform clientIcon; // The RectTransform of the client icon

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

    public bool lightConfirmed = false;
    public bool darkConfirmed = false;

    private GameObject player1;
    private GameObject player2;
    private GameObject selectedPlayer;

    public static int hostSelection = 0;
    public static bool player1Confirm = false;
    public static bool player2Confirm = false;
    public static bool confirm = false;
    private ulong clientId;

    private bool isInMiddle = true;
    private bool isInMiddleClient = true;
    private bool DarkSelected = false;
    private bool LightSelected = false;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        // Initialize player GameObjects
        player1 = GameObject.FindGameObjectWithTag("DarkSelect");
        player2 = GameObject.FindGameObjectWithTag("LightSelect");

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
    }

    public void ResetPositions()
    {
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
    }

    void Update()
    {
        //Set proper buttons/images
        if (NetworkManager.Singleton.LocalClientId == 0)
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
        }

        clientId = NetworkManager.Singleton.LocalClientId;

        if (selectedPlayer == player1 && Input.GetKeyDown(KeyCode.X))
        {
            darkReady.SetActive(true);
            darkConfirmed = true;
        }

        if (selectedPlayer == player2 && Input.GetKeyDown(KeyCode.X))
        {
            lightReady.SetActive(true);
            lightConfirmed = true;
        }

        //    if (Confirm(0) && Confirm(1)) //(Confirm(0) && (clientId == 1 && Confirm(1)))

        if (lightConfirmed && darkConfirmed)
        {
            Debug.Log("both players confirmed");
            UIManager.closePlayerSelect = true;
            confirm = true;
            darkConfirmed = false;
            lightConfirmed = false;
            uiManager.ShowGameUI();
            SceneManager.LoadScene("BUILD-SCENE");
        }
    }

    private void MoveLeft()
    {
        if (isInMiddle && !DarkSelected) // Move to left only if currently in middle
        {
            Debug.Log("Move left - From middle to left");

            hostIcon.position = leftPosition.position;
            rightArrow.position = rightArrowLeft.position;
            leftArrow.gameObject.SetActive(false);
            darkLight.SetActive(true);

            isInMiddle = false;
            DarkSelected = true;
            Selected(player1);

            hostSelection = 1;
            selectedPlayer = player1;
        }
        else if (hostIcon.position == rightPosition.position) // If on right, move to middle
        {
            Debug.Log("Move left - From Right to Middle");
            selectedPlayer = null;
            hostIcon.position = middlePosition.position;
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
            Debug.Log("Move right - From middle to right");

            hostIcon.position = rightPosition.position;
            leftArrow.position = leftArrowRight.position;
            rightArrow.gameObject.SetActive(false);
            lightLight.SetActive(true);

            isInMiddle = false;
            LightSelected = true;

            Selected(player2);

            hostSelection = 0;
            selectedPlayer = player2;
        }
        else if (hostIcon.position == leftPosition.position) // If on left, move to middle
        {
            Debug.Log("Move right - From Left to Middle");

            selectedPlayer = null;
            hostIcon.position = middlePosition.position;
            isInMiddle = true;
            LightSelected = false;

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
        }
        else if (clientIcon.position == rightPositionClient.position) // If on right, move to middle
        {
            Debug.Log("CLIENT Move left - From Right to Middle");

            selectedPlayer = null;
            clientIcon.position = middlePositionClient.position;
            isInMiddleClient = true;
            DarkSelected = false;

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
        }
        else if (clientIcon.position == leftPositionClient.position) // If on left, move to middle
        {
            Debug.Log("CLIENT Move right - From Left to Middle");

            selectedPlayer = null;
            clientIcon.position = middlePositionClient.position;
            isInMiddleClient = true;
            LightSelected = false;

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

    private bool Confirm(ulong clientID)
    {
        if (selectedPlayer != null)
        {
            if (clientId == 0)
            {
                player1Confirm = true;
                return true;
            }

            if (clientId == 1)
            {
                player2Confirm = true;
                return true;
            }
        }
        return false;
    }
}
