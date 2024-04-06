using UnityEngine;

namespace Scenes.Ally
{
    public class SuperBlasterCombo : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Hammer"))
            {
                // Debug.Log("here!!!!!!!!!: " + other.gameObject + " and " + this.gameObject);
                ComboAttackManager.SpawnSuperBlaster();
            }
        }
    }
}
