using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
using TMPro;

public class DCore : NetworkBehaviour
{
    // I created a custom event which will be triggered when the core is destroyed
    public delegate void DCoreDelegate();
    public event DCoreDelegate OnCoreDestroyed;
    public event Action<float> OnHealthChanged; // Added this line for changing the health bar

    // default health for a dream core is 100
    public int health = 100;

    private int healers = 0;

    [SerializeField]
    private TextMeshProUGUI Warning;

    private int warningTimer = 0 ;

    // public getter method for health
    public int GetHealth
    {
        get { return health; }
    }

    // dream core add itself to the dream core list managed by soTargetManager
    // [SerializeField]
    // private SO_TargetManager soTargetManager;

   
    private void Awake()
    {
        Warning.enabled = false;

        // when OnCoreDestroyed event is triggered, run the DestructibleByEnemy_OnDestroyed method
        // (here I assign a method to a event)
        OnCoreDestroyed += DestructibleByEnemy_OnDestroyed;

        // adding itself to the dream core list manager by soTargetManager
        SO_TargetManager.AddDCore(this);
        GameManager.AddToMasterDCoreList(this);
    }

    public void DCoreMasterReset()
    {
        SO_TargetManager.AddDCore(this);
        health = 100;
    }

    public override void OnDestroy()
    {
        // we need to de-assign methods otherwise you may see errors
        OnCoreDestroyed -= DestructibleByEnemy_OnDestroyed;
    }

    private void DestructibleByEnemy_OnDestroyed()
    {
        // first remove itself from the dream core list managed by soTargetManager
        SO_TargetManager.RemoveDCore(this);
        // then destroy itself
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }

    [ServerRpc]
    void GetDamageServerRpc(int amount)
    {
        health -= amount;
        ColorDegradation.UpdateGlobalHP(amount);
        //Debug.Log(health);
        OnHealthChanged?.Invoke((float)health / 1); // Invoke the event, passing the current health percentage
        Warning.enabled = true;
        warningTimer = 100;
        // dream core gets damage with this method
        // it returns true if the dream core is destroyed
        // or it returns false if dream core is still alive after the damage
    }

  
    private float damage_tick_rate = 1.5f;
    private float damage_time = 0;
    
    public bool GetDamage(int amount)
    {
        if (Time.time > damage_time)
        {
            damage_time = Time.time + damage_tick_rate;
        }
        else
        {
            return false;
        }
        
        if (IsServer)
            GetDamageServerRpc(amount);

        if (health < 0)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPlayer") || other.CompareTag("DarkPlayer"))
        {
            healers += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightPlayer") || other.CompareTag("DarkPlayer"))
        {
            healers -= 1;
        }
    }

    // Why snake case :hands:
    // Why define variables not at the top :hands:
    private float heal_tick_rate = 0.5f;
    private float heal_time = 0;

    private void FixedUpdate()
    {
        if (healers > 0)
        {
            if (Time.time > heal_time)
            {
                health += 15;
                health = Math.Min(100, health);
                heal_time = Time.time + heal_tick_rate;
            }
        }

        if (warningTimer > 0)
        {
            warningTimer--;
        }

        if (warningTimer <= 0)
        {
            Warning.enabled = false;
        }
    }
}
