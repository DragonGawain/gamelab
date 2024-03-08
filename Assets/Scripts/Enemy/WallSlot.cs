using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlot: MonoBehaviour
{
    public bool isFilled;
    public GameObject wallObject;
    private int wallHealth = 100;
    public int GetHealth
    {
        get { return wallHealth; }
    }
    [SerializeField] private Collider wallCollider;
    // management
    [SerializeField] private SO_TargetManager soTargetManager;

    private void Awake()
    {
        soTargetManager.AddWallSlot(this);
    }
    public bool GetDamage(int amount)
    {
        // returns true if the wall is destroyed
        if(wallHealth - amount <= 0)
        {
            wallObject.SetActive(false);
            soTargetManager.BakeNavmesh();
            return true;
        }
        else
        {
            wallHealth -= amount;
            return false;
        }
    }

}
