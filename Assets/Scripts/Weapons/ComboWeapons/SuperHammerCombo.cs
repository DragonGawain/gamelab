using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class SuperHammerCombo : Hammer
    {

        private void OnTriggerStay(Collider other)
        {

            if (other.gameObject.CompareTag("FlameHitBox"))
            {
                //Debug.Log("here!!!!!!!!!: " + other.gameObject + " and " + this.gameObject);
                Flamethrower flamethrower = other.gameObject.GetComponentInParent<Flamethrower>();
                if (flamethrower.firing)
                    ComboAttackManager.SpawnSuperHammer();
            }
        }

    }
}
