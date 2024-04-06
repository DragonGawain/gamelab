using System.Collections;
using System.Collections.Generic;
using Players;
using Weapons;
using UnityEngine;
using Unity.VisualScripting;

namespace Weapons
{
    public class DOTCloud : MonoBehaviour
    {
        static readonly float maxSize = 8.0f;
        static readonly int dmg = 15;
        int deathTimer = 200;
        int timeToMaxSize = 150;
        int timer = 0;
        float currentScale = 1;
        List<Enemy> inContactWith = new();

        // This FixedUpdate method is the old way
        // public void FixedUpdate()
        // {
        //     Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius / 2);
        //     foreach (Collider hit in colliders)
        //     {
        //         if (!hit.CompareTag("ComboEnemy"))
        //             continue;
        //         // Check if the collider belongs to an enemy
        //         hit.GetComponent<Enemy>().OnHit(25, "DarkPlayer");

        //         Rigidbody rb = hit.GetComponent<Rigidbody>();
        //         if (rb != null)
        //         {
        //             rb.AddExplosionForce(100, transform.position, blastRadius);
        //         }
        //     }
        // }


        // When an object is destroyed, OnTriggerExit is NOT called, so I instead keep track of all enemies this DOTCloud is incontact with
        // When the cloud expires, I state that all enemies are no longer taking DOT damage
        private void FixedUpdate()
        {
            deathTimer--;
            if (deathTimer <= 0)
            {
                foreach (Enemy e in inContactWith)
                {
                    e.SetIsTakingDOTDamage(false);
                }
                Destroy(this.gameObject);
            }

            if (timer < timeToMaxSize)
                timer++;

            currentScale = Mathf.Lerp(1, maxSize, (float)timer / (float)timeToMaxSize);
            transform.localScale = new(currentScale, 1, currentScale);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ComboEnemy"))
            {
                inContactWith.Add(other.GetComponent<Enemy>());
                other.GetComponent<Enemy>().SetIsTakingDOTDamage(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ComboEnemy"))
            {
                inContactWith.Remove(other.GetComponent<Enemy>());
                other.GetComponent<Enemy>().SetIsTakingDOTDamage(false);
            }
        }

        public static int GetDotDamage()
        {
            return dmg;
        }
    }
}
