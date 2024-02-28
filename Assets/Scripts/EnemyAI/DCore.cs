using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCore : MonoBehaviour
{
    public delegate void DCoreDelegate();
    public event DCoreDelegate OnCoreDestroyed;
    private int health = 100;

    public int GetHealth
    {
        get { return health; }
    }

    [SerializeField] private SO_TargetManager soTargetManager;
    private void Awake()
    {
        OnCoreDestroyed += DestructibleByEnemy_OnDestroyed;

        soTargetManager.AddDCore(this);
    }

    private void OnDestroy()
    {
        OnCoreDestroyed -= DestructibleByEnemy_OnDestroyed;
    }

    private void DestructibleByEnemy_OnDestroyed()
    {
        soTargetManager.RemoveDCore(this);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(transform.up, 20 * Time.deltaTime);
    }

    public bool GetDamage(int amount)
    {
        health -= amount;
        if(health < 0)
        {
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
