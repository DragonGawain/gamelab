using UnityEngine;

namespace Weapons.ComboWeapons
{
    public class SuperBlasterCombo : MonoBehaviour
    {
        [SerializeField] AudioSource SuperBlasterAudio;

        public void Start()
        {

            SuperBlasterAudio = GetComponent<AudioSource>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hammer"))
                ComboAttackManager.SpawnSuperBlaster();
            SuperBlasterAudio.Play();
        }
    }
}
