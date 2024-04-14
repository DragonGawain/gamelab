using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;


public class SelectPlayer : NetworkBehaviour
{

    private UIManager uiManager;

    [SerializeField] private Transform selectionIcon; // The transform of the selection icon
    [SerializeField] private Transform leftPosition; // Position for Player 1 selection
    [SerializeField] private Transform middlePosition; // Position for no selection
    [SerializeField] private Transform rightPosition; // Position for Player 2 selection
    [SerializeField] private Transform leftArrow;
    [SerializeField] private Transform rightArrow;
    [SerializeField] private Transform leftArrowMiddle;
    [SerializeField] private Transform rightArrowMiddle;
    [SerializeField] private Transform leftArrowRight;
    [SerializeField] private Transform rightArrowLeft;
    [SerializeField] private GameObject darkLight;
    [SerializeField] private GameObject lightLight;
    [SerializeField] private GameObject darkReady;
    [SerializeField] private GameObject lightReady;

    public bool lightConfrimed = false;
    public bool darkConfrimed = false;


    private GameObject player1;
    private GameObject player2;
    private GameObject selectedPlayer;

    public static int hostSelection = 0;
    public static bool player1Confirm = false;
    public static bool player2Confirm = false;
    public static bool confirm = false;
    private ulong clientId;

    private bool isInMiddle = true;

    void Start()
    {


        uiManager = FindObjectOfType<UIManager>();

        // Initialize player GameObjects
        player1 = GameObject.FindGameObjectWithTag("DarkSelect");
        player2 = GameObject.FindGameObjectWithTag("LightSelect");
        selectionIcon.position = middlePosition.position; // Start in the middle
        leftArrow.position = leftArrowMiddle.position; // Start in the middle
        rightArrow.position = rightArrowMiddle.position; // Start in the middle
        lightLight.SetActive(false);
        darkLight.SetActive(false);
        lightReady.SetActive(false);
        darkReady.SetActive(false);

    }

    void Update()
    {
        clientId = NetworkManager.Singleton.LocalClientId;
        //Debug.Log("Host:" + Confirm(0));
        //Debug.Log("Client:" + Confirm(1));


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (selectedPlayer == player1 && Input.GetKeyDown(KeyCode.X))
        {
            darkReady.SetActive(true);
            darkConfrimed = true;
        }

        if (selectedPlayer == player2 && Input.GetKeyDown(KeyCode.X))
        {
            lightReady.SetActive(true);
            lightConfrimed = true;
        }


        {

        if (lightConfrimed && darkConfrimed)
            {
                Debug.Log("both players confirmed");
                UIManager.closePlayerSelect = true;
                confirm = true;
                uiManager.ShowGameUI();

            }
                
            
        }

    }

    private void MoveLeft()
    {
        if (isInMiddle) // Move to left only if currently in middle
        {
            selectionIcon.position = leftPosition.position;
            rightArrow.position = rightArrowLeft.position;
            leftArrow.gameObject.SetActive(false);
            darkLight.SetActive(true);

            isInMiddle = false;
            Selected(player1);


            if (clientId == 0)
            {
                hostSelection = 1;
            }
            selectedPlayer = player1;

        }
        else if (selectionIcon.position == rightPosition.position) // If on right, move to middle
        {

            selectedPlayer = null;
            selectionIcon.position = middlePosition.position;
            isInMiddle = true;
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
        if (isInMiddle) // Move to right only if currently in middle
        {
            selectionIcon.position = rightPosition.position;
            leftArrow.position = leftArrowRight.position;
            rightArrow.gameObject.SetActive(false);
            lightLight.SetActive(true);

            isInMiddle = false;
            Selected(player2);

            if (clientId == 0)
            {
                hostSelection = 0;
            }
            selectedPlayer = player2;

        }
        else if (selectionIcon.position == leftPosition.position) // If on left, move to middle
        {

            selectedPlayer = null;
            selectionIcon.position = middlePosition.position;
            isInMiddle = true;
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
        if(selectedPlayer != null)
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
