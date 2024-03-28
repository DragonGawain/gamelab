using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ComboHitBox;
using static ComboAttackManager;
using static Enemy;


public class Enemy2 : Enemy
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
        if (attackType == ComboAttackType.SuperBullet)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(this.gameObject);

            }
        }
    }
}