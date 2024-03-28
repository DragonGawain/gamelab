using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ComboHitBox;
using static ComboAttackManager;
using static Enemy;
using Weapons;

public class Enemy5 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHitByCombo(ComboAttackType attackType, int damage)
    {
        if (attackType == ComboAttackType.SuperHammer)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(this.gameObject);

            }
        }
    }
   
}