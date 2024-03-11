using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTCloud : MonoBehaviour
{
    int deathTimer = 150;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        deathTimer--;
        if (deathTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
