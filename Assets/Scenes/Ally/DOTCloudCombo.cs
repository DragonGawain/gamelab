using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class DOTCloudCombo : Blaster
    {

        private void OnTriggerEnter(Collider other)
        {

            //YES YOU CAN
            //Debug.Log("do i get here even?????");

            if (other.gameObject.CompareTag("Grenade"))
            {
                Grenade grenade = other.gameObject.GetComponent<Grenade>();
                if (grenade.isExploding == false) {
               

                    Debug.Log("here  ZAID: " + other.gameObject + " and " + this.gameObject);

                ComboAttackManager.SpawnDOTCloud(other.gameObject);
                }
            }
            else
            {
                //Debug.Log("WTFFFFF: " + other.gameObject + " and " + this.gameObject);

            }

        }

    }
}


