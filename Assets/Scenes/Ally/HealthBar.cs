using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
 [SerializeField] private PlayerTestScript player; // Reference to the player
 [SerializeField] private float fillAmount;
[SerializeField] private Image health;


    private void Start()
    {
        if (player != null)
        {
            player.OnHealthChanged += UpdateHealthBar;
        }
        else
        {
            Debug.LogWarning("Player reference not set in HealthBar script.");
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.OnHealthChanged -= UpdateHealthBar;
        }
    }



    private void UpdateHealthBar(float healthPercentage)
    {
        fillAmount = healthPercentage; // Update the fillAmount based on the event data
        HandleBar();
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



    private void CheckHealthAndExit()
    {
        if (fillAmount == 0) // Check if fillAmount is essentially 0
        {
            Debug.Log("Player was killed");
          // game over or respawn?
        }
    }
}
