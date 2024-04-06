using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    [SerializeField] private Transform selectionIcon; // The transform of the selection icon
    [SerializeField] private Transform leftPosition; // Position for Player 1 selection
    [SerializeField] private Transform middlePosition; // Position for no selection
    [SerializeField] private Transform rightPosition; // Position for Player 2 selection

    private GameObject player1;
    private GameObject player2;

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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        if (isInMiddle) // Move to left only if currently in middle
        {
            selectionIcon.position = leftPosition.position;
            isInMiddle = false;
            Selected(player1);
        }
        else if (selectionIcon.position == rightPosition.position) // If on right, move to middle
        {
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
        }
        else if (selectionIcon.position == leftPosition.position) // If on left, move to middle
        {
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
        }
        else
        {
            Debug.Log("im not animated");
        }
    }
}
