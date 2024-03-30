using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class SuperHammerCombo : Hammer
    {

        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("do i get here even 2222?????");

            if (other.gameObject.CompareTag("FireParticles"))
            {
                Debug.Log("here!!!!!!!!!: " + other.gameObject + " and " + this.gameObject);

                ComboAttackManager.SpawnSuperHammer();

            }
            else
            {
                //Debug.Log("WTFFFFF: " + other.gameObject + " and " + this.gameObject);

            }

        }

    }
}
