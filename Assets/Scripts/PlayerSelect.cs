using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public GameObject currentPlayer;
    public GameObject otherPlayer;
    public Transform frontLocation; // Assign this in the inspector to the front location empty GameObject
    public Transform backLocation;  // Assign this in the inspector to the back location empty GameObject

    void Start()
    {
        // Initialize current and other players if not set in inspector
        if (currentPlayer == null)
            currentPlayer = GameObject.FindGameObjectWithTag("DarkSelect");
        if (otherPlayer == null)
            otherPlayer = GameObject.FindGameObjectWithTag("LightSelect");
    }

    void Update()
    {
        // Check for player toggle input (e.g., pressing the 'Space' key)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePlayer();
        }
    }

    private void TogglePlayer()
    {
        // Swap the positions by moving the current player to the back and the other player to the front
        currentPlayer.transform.position = backLocation.position;
        otherPlayer.transform.position = frontLocation.position;

        // Swap references so the other player becomes the current one and vice versa
        GameObject temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;


        // Play celebrate animation on the new current (front) player
        Animator currentAnimator = currentPlayer.GetComponent<Animator>();
        if (currentAnimator != null)
        {
            Debug.Log("Playing celebrate animation for " + currentPlayer.name);
            currentAnimator.SetTrigger("Celebrate");
        }
        else
        {
            Debug.LogError("Animator not found on " + currentPlayer.name);
        }
    }
}
