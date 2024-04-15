using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class DOTCloudCombo : MonoBehaviour
    {
        [SerializeField] AudioSource DOTAudio;

        public void Start()
        {

            DOTAudio = GetComponent<AudioSource>();

        }

        private void OnTriggerStay(Collider other)
        {

            //YES YOU CAN, nvm, nvm again

            if (other.gameObject.CompareTag("Grenade"))
            {
                Grenade grenade = other.gameObject.GetComponent<Grenade>();
                Flamethrower flamethrower = GetComponentInParent<Flamethrower>();
                if (grenade.isExploding == false && flamethrower.firing) {
               
                    
                    //Debug.Log("here  ZAID: " + other.gameObject + " and " + this.gameObject);

                    ComboAttackManager.SpawnDOTCloud(other.gameObject);
                    DOTAudio.Play();

                }
                else
                {
                    //Debug.Log("Grenade Exploding: " + grenade.isExploding + ", Flamethrower firing: " + flamethrower.firing);
                }
            }
            else
            {
                //Debug.Log("WTFFFFF: " + other.gameObject + " and " + this.gameObject);

            }

        }

    }
}


