using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{

    private GameObject currentPlayer;
    private GameObject otherPlayer;


    void Start()
    {

        // Initialize current and other players
        currentPlayer = GameObject.FindGameObjectWithTag("DarkSelect");
        otherPlayer = GameObject.FindGameObjectWithTag("LightSelect");
    }

    // Update is called once per frame
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
        // Swap references
        GameObject temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;

        // Swap positions
        Vector3 tempPosition = currentPlayer.transform.position;
        currentPlayer.transform.position = otherPlayer.transform.position;
        otherPlayer.transform.position = tempPosition;

        // Play celebrate animation on the current (front) player
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