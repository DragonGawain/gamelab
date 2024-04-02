using UnityEngine;

namespace Scenes.Ally
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
