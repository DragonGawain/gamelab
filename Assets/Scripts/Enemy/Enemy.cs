using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Players;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision other)
    {
        // Been hit by blaster
        
        if (other.gameObject.name == "Bullet(Clone)")
        {
            Destroy(other.gameObject);
        }
        
        //Debug.Log(other.body.name);
    }
}
