using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;


namespace Weapons
{
    public class DOTCloud : GrenadeLauncher
    {
        int deathTimer = 150;
        // Start is called before the first frame update
        void Start()
        {
            SetDamage(50);
            SetWeaponName("DOTCloud");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            deathTimer--;
            if (deathTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}