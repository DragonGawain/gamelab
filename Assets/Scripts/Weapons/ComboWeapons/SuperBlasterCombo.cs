using UnityEngine;

namespace Weapons.ComboWeapons
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
