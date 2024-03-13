using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

[SerializeField] private float fillAmount;
[SerializeField] private Image health;

private bool hasDecreasedHealth = false; // Flag to track if health has already been decreased




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
        CheckHealthAndExit();

    }

    private void HandleBar()
    {
        health.fillAmount = fillAmount;
    }


    // This method is called when another collider marked as a Trigger touches this object
    private void OnTriggerEnter(Collider other)
    { 

       if (other.CompareTag("TargetWall") && !hasDecreasedHealth) // Check if the collision object has the tag 
        {
            Debug.Log("hit!");
            fillAmount -= 0.1f; // Reduce the fill amount by 0.1
            fillAmount = Mathf.Clamp(fillAmount, 0f, 1f); // Ensure fillAmount stays within 0 and 1
            HandleBar(); // Update the health bar immediately
            hasDecreasedHealth = true; // Set the flag to true to prevent multiple decreases

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TargetWall")) // Check if the exiting object has the tag 'House'
        {
            hasDecreasedHealth = false; // Reset the flag when the player exits the trigger area, allowing health to decrease again on re-entry
        }
    }


    private void CheckHealthAndExit()
    {
        if (fillAmount == 0) // Check if fillAmount is essentially 0
        {
          // game over or respawn?
        }
    }
}
