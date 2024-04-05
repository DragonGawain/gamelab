using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // layer 8 => bullet layer (for bullet, super bullet, bullet barage, etc)
        if (other.gameObject.layer == 8)
            Destroy(other.gameObject);
    }
}
