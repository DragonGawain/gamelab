using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class CrystalBar : MonoBehaviour
    {
        public float health;
        public float maxHealth;
        public GameObject healthBarUI;
        public Slider slider;
        DCore core;
        private bool isInsideTrigger = false; // Flag to track whether the player is inside the trigger

        void Start()
        {
            maxHealth = 100;
            health = maxHealth;
            slider.value = 100;

            // DCore core = FindObjectOfType<DCore>(); // Find the DCore in the scene
            core = GetComponent<DCore>();
            if (core != null)
            {
                core.OnHealthChanged += UpdateHealthBar; // Subscribe to the OnHealthChanged event
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("LightPlayer"))
            {
                StopAllCoroutines(); // Stop the losing health coroutine
                StartCoroutine(GainHealthOverTime()); // Start gaining health coroutine
                isInsideTrigger = true; // Set the flag to true
            }
            if (other.CompareTag("DarkPlayer"))
            {
                StopAllCoroutines(); // Stop the losing health coroutine
                StartCoroutine(GainHealthOverTime()); // Start gaining health coroutine
                isInsideTrigger = true; // Set the flag to true
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("LightPlayer"))
            {
                StopAllCoroutines(); // Stop the gaining health coroutine
                // StartCoroutine(LoseHealthOverTime()); // Start losing health coroutine
                isInsideTrigger = false; // Reset the flag to false
            }

            if (other.CompareTag("DarkPlayer"))
            {
                StopAllCoroutines(); // Stop the gaining health coroutine
                // StartCoroutine(LoseHealthOverTime()); // Start losing health coroutine
                isInsideTrigger = false; // Reset the flag to false
            }
        }

        private IEnumerator GainHealthOverTime()
        {
            while (isInsideTrigger)
            {
                health += 0.1f;
                yield return new WaitForSeconds(1f); // Wait for 1 seconds before the next health gain
            }
        }

        private void UpdateHealthBar(float newHealth)
        {
            slider.value = newHealth;
            //Debug.Log("NEW " + newHealth);
            //Debug.Log("SLV " + slider.value);

            if (health < maxHealth)
            {
                healthBarUI.SetActive(true);
            }
            if (health <= 0)
            {
                Debug.Log("DESTROYED BY CRYSTAL BAR");
                // Destroy(gameObject);
            }
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        //float CalculateHealth()
        //{
        //    return health / maxHealth;
        //}
    }
}
