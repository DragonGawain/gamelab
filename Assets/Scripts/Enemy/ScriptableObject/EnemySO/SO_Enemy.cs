using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu written for creating the ScriptableObjects based on this structure
[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class SO_Enemy : ScriptableObject
{
    // I used scriptable objects for enemy types.
    // You can think of this as a structure for enemy types, what are the things that will be different between different enemy types.
    // It consist of speed, maximum health, and damage information right now.
    // however, you can extend this. for example, you can set references to specific visual effects for enemies.
    // or you can even set different sounds for them, etc.

    // I set variables as private to avoid any other scripts change them.
    // I Serialized them to change from Inspector.
    // and I wrote public getter to use them in enemy logic.
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
    }

    [SerializeField] private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
    }


    [SerializeField] private int damage;
    public int Damage
    {
        
        get { return 3; }
    }
    

    

}
