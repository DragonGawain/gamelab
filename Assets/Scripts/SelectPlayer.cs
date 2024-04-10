using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

/*
 * SET UP HAS TO BE WORKED WITH NETWORKING
 * 
 * CURRENTLY ONLY FUNCTIONS FOR ONE PLAYER
 * 
 * ADD A CONFIRMATION TO SAVE SELECTION 
 * 
 */


public class SelectPlayer : NetworkBehaviour
{
    [SerializeField] private Transform selectionIcon; // The transform of the selection icon
    [SerializeField] private Transform leftPosition; // Position for Player 1 selection
    [SerializeField] private Transform middlePosition; // Position for no selection
    [SerializeField] private Transform rightPosition; // Position for Player 2 selection

    private GameObject player1;
    private GameObject player2;
    private GameObject selectedPlayer;
    private GameObject selectedPlayer2;

    public static int hostSelection = 0;
    public static bool player1Confirm = false;
    public static bool player2Confirm = false;
    public static bool confirm = false;
    private ulong clientId;

    private bool isInMiddle = true;

    void Start()
    {
        // Initialize player GameObjects
        player1 = GameObject.FindGameObjectWithTag("DarkSelect");
        player2 = GameObject.FindGameObjectWithTag("LightSelect");
        selectionIcon.position = middlePosition.position; // Start in the middle
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

        if (!isInMiddle && Input.GetKeyDown(KeyCode.X))
        {
            if(Confirm(clientId))
            {
                UIManager.closePlayerSelect = true;
            }

            if (Confirm(0) && Confirm(1)) //(Confirm(0) && (clientId == 1 && Confirm(1)))
            {
                Debug.Log("both players confirmed");
                confirm = true;
            }
        }

    }

    private void MoveLeft()
    {
        if (isInMiddle) // Move to left only if currently in middle
        {
            selectionIcon.position = leftPosition.position;
            isInMiddle = false;
            Selected(player1);

            if (clientId == 0)
            {
                hostSelection = 1;
                selectedPlayer = player1;
            }
            else
            {
                selectedPlayer2 = player1;
            }

        }
        else if (selectionIcon.position == rightPosition.position) // If on right, move to middle
        {
            selectedPlayer = null;
            selectedPlayer2 = null;
            selectionIcon.position = middlePosition.position;
            isInMiddle = true;
        }
    }

    private void MoveRight()
    {
        if (isInMiddle) // Move to right only if currently in middle
        {
            selectionIcon.position = rightPosition.position;
            isInMiddle = false;
            Selected(player2);

            if (clientId == 0)
            {
                hostSelection = 0;
                selectedPlayer = player2;
            }
            else
            {
                selectedPlayer2 = player2;
            }

        }
        else if (selectionIcon.position == leftPosition.position) // If on left, move to middle
        {
            selectedPlayer = null;
            selectedPlayer2 = null;
            selectionIcon.position = middlePosition.position;
            isInMiddle = true;
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
        }

        if(selectedPlayer2 != null)
        {
            if (clientId == 1)
            {
                player2Confirm = true;
                return true;
            }
        }

        return false;
    }
}
