using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
 [SerializeField] private PlayerTestScript player; // Reference to the player


    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;

    private void Start()
    {

        health = maxHealth;
        slider.value = 100;

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


    private void UpdateHealthBar(float newHealth)
    {
        slider.value = newHealth;
        Debug.Log("PLAYER NEW " + newHealth);
        Debug.Log("PLAYER SLV " + slider.value);

        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);

        }
        if (health <= 0)
        {
            Debug.Log("Player was killed");

        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }




   
}
