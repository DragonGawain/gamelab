using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrystalBar : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    [SerializeField] private Image health;
    private bool isInsideTrigger = false; // Flag to track whether the player is inside the trigger

    void Start()
    {
        StartCoroutine(LoseHealthOverTime()); // Start losing health over time by default
    }

    void Update()
    {
        HandleBar();
        CheckHealthAndExit();
    }

    private void HandleBar()
    {
        health.fillAmount = fillAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines(); // Stop the losing health coroutine
            StartCoroutine(GainHealthOverTime()); // Start gaining health coroutine
            isInsideTrigger = true; // Set the flag to true
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines(); // Stop the gaining health coroutine
            StartCoroutine(LoseHealthOverTime()); // Start losing health coroutine
            isInsideTrigger = false; // Reset the flag to false
        }
    }

    private IEnumerator GainHealthOverTime()
    {
        while (isInsideTrigger)
        {
            fillAmount += 0.1f;
            fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
            HandleBar();
            yield return new WaitForSeconds(2f); // Wait for 2 seconds before the next health gain
        }
    }

    private IEnumerator LoseHealthOverTime()
    {
        while (!isInsideTrigger)
        {
            fillAmount -= 0.1f;
            fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
            HandleBar();
            yield return new WaitForSeconds(3f); // Wait for 3 seconds before the next health reduction
        }
    }

    private void CheckHealthAndExit()
    {
        if (fillAmount <= 0) // Check if fillAmount is essentially 0
        {
            // Implement game over or respawn logic here
            fillAmount = 0; // Ensure fillAmount doesn't go below 0
            HandleBar();
            // Example: Application.Quit(); or initiate respawn sequence
        }
    }
}
