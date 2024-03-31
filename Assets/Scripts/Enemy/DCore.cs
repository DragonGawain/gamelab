using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DCore : MonoBehaviour
{
    // I created a custom event which will be triggered when the core is destroyed
    public delegate void DCoreDelegate();
    public event DCoreDelegate OnCoreDestroyed;
    public event Action<float> OnHealthChanged; // Added this line for changing the health bar

    // default health for a dream core is 100
    private float health = 100;

    // public getter method for health
    public float GetHealth
    {
        get { return health; }
    }

    // dream core add itself to the dream core list managed by soTargetManager
    [SerializeField] private SO_TargetManager soTargetManager;
    private void Awake()
    {
        // when OnCoreDestroyed event is triggered, run the DestructibleByEnemy_OnDestroyed method
        // (here I assign a method to a event)
        OnCoreDestroyed += DestructibleByEnemy_OnDestroyed;

        // adding itself to the dream core list manager by soTargetManager
        soTargetManager.AddDCore(this);
    }

    private void OnDestroy()
    {
        // we need to de-assign methods otherwise you may see errors
        OnCoreDestroyed -= DestructibleByEnemy_OnDestroyed;
    }

    private void DestructibleByEnemy_OnDestroyed()
    {
        // first remove itself from the dream core list managed by soTargetManager
        soTargetManager.RemoveDCore(this);
        // then destroy itself
        Destroy(this.gameObject);
    }
    
    public bool GetDamage(float amount)
    {
        health -= amount;
        Debug.Log(health);
        OnHealthChanged?.Invoke((float)health / 1); // Invoke the event, passing the current health percentage

        // dream core gets damage with this method
        // it returns true if the dream core is destroyed
        // or it returns false if dream core is still alive after the damage
        if(health < 0)
        {
            // triggering the event
            OnCoreDestroyed();
            return true;
        }
        else
        {
            //Debug.Log($"health left: {health}");
            return false;
        }
    }
}
