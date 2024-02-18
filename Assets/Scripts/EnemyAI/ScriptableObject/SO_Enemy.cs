using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class SO_Enemy : ScriptableObject
{

    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;


    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
    }
    

    

}
