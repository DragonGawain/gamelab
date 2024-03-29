using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ComboHitBox;
using static ComboAttackManager;
using static Enemy;
using Weapons;


public class Enemy3 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //grenade launcher and bullet blaster

    public override void OnHitByCombo(ComboAttackType attackType, int damage)
    {
        if (attackType == ComboAttackType.BulletBarrage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(this.gameObject);

            }
        }
    }
}