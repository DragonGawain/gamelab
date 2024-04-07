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
            if (other.gameObject.CompareTag("Hammer"))
                ComboAttackManager.SpawnSuperBlaster();
        }
    }
}
