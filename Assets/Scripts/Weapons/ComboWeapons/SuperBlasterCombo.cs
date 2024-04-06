using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class SuperBlasterCombo : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {

        
            Debug.Log("Ally is touching " + other.name);

            if (other.gameObject.CompareTag("Hammer"))
            {
                Debug.Log("Ally is inside things");            
                // Debug.Log("here!!!!!!!!!: " + other.gameObject + " and " + this.gameObject);

                ComboAttackManager.SpawnSuperBlaster();
            

            }

        }
    }
}
