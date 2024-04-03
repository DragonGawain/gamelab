using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class BulletBarrageCombo : Blaster
    {

        private void OnTriggerEnter(Collider other)
        {

            //YES YOU CAN
            //Debug.Log("do i get here even?????");

            if (other.gameObject.CompareTag("Grenade"))
            {
                Debug.Log("here!!!!!!!!!: " + other.gameObject + " and " + this.gameObject);

                ComboAttackManager.SpawnBulletBarrage(other.gameObject, this.gameObject);
              
            }
            else
            {
                //Debug.Log("WTFFFFF: " + other.gameObject + " and " + this.gameObject);

            }

        }

    }
}
