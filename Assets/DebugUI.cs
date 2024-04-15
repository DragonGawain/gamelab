using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hostHealth;
    [SerializeField] private TextMeshProUGUI clientHealth;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetClientHealth(int health)
    {
        clientHealth.text = health.ToString();
    }

    public void SetHostHealth(int health)
    {
        hostHealth.text = health.ToString();
    }
    
}
