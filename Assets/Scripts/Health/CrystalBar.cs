//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//public class CrystalBar : MonoBehaviour
//{
//    [SerializeField] private float fillAmount;
//    [SerializeField] private Image health;
//    private bool isInsideTrigger = false; // Flag to track whether the player is inside the trigger

//    void Start()
//    {
//        DCore core = FindObjectOfType<DCore>(); // Find the DCore in the scene
//        if (core != null)
//        {
//            core.OnHealthChanged += UpdateHealthBar; // Subscribe to the OnHealthChanged event
//        }
//    }

//    private void UpdateHealthBar(float healthPercentage)
//    {
//        fillAmount = healthPercentage; // Update the fillAmount based on the event data
//        HandleBar();
//    }

//    void Update()
//    {
//        HandleBar();
//        CheckHealthAndExit();
//    }

//    private void HandleBar()
//    {
//        health.fillAmount = fillAmount;
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("LightPlayer"))
//        {
//            StopAllCoroutines(); // Stop the losing health coroutine
//            StartCoroutine(GainHealthOverTime()); // Start gaining health coroutine
//            isInsideTrigger = true; // Set the flag to true
//        }
//        if (other.CompareTag("DarkPlayer"))
//        {
//            StopAllCoroutines(); // Stop the losing health coroutine
//            StartCoroutine(GainHealthOverTime()); // Start gaining health coroutine
//            isInsideTrigger = true; // Set the flag to true
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("LightPlayer"))
//        {
//            StopAllCoroutines(); // Stop the gaining health coroutine
//           // StartCoroutine(LoseHealthOverTime()); // Start losing health coroutine
//            isInsideTrigger = false; // Reset the flag to false
//        }

//        if (other.CompareTag("DarkPlayer"))
//        {
//            StopAllCoroutines(); // Stop the gaining health coroutine
//           // StartCoroutine(LoseHealthOverTime()); // Start losing health coroutine
//            isInsideTrigger = false; // Reset the flag to false
//        }
//    }


//    private IEnumerator GainHealthOverTime()
//    {
//        while (isInsideTrigger)
//        {
//            fillAmount += 0.1f;
//            fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
//            HandleBar();
//            yield return new WaitForSeconds(2f); // Wait for 2 seconds before the next health gain
//        }
//    }

//    //private IEnumerator LoseHealthOverTime()
//    //{
//    //    while (!isInsideTrigger)
//    //    {
//    //        fillAmount -= 0.1f;
//    //        fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
//    //        HandleBar();
//    //        yield return new WaitForSeconds(3f); // Wait for 3 seconds before the next health reduction
//    //    }
//    //}

//    private void CheckHealthAndExit()
//    {
//        if (fillAmount <= 0) // Check if fillAmount is essentially 0
//        {
//            // Implement game over or respawn logic here
//            fillAmount = 0; // Ensure fillAmount doesn't go below 0
//            HandleBar();
//            // Example: Application.Quit(); or initiate respawn sequence
//        }
//    }
//}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CrystalBar : MonoBehaviour {

    public float health; public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;
   private bool isInsideTrigger = false; // Flag to track whether the player is inside the trigger

    void Start()
    {
        health = maxHealth;
        slider.value = 100;

        DCore core = FindObjectOfType<DCore>(); // Find the DCore in the scene
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
            Destroy(gameObject);

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
