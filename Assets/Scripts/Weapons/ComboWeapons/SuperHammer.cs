using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHammer : MonoBehaviour
{
    int deathTimer = 150;
    static SuperHammer instance;
    // Start is called before the first frame update
    void Awake()
    {
        // singleton pattern - there can only be a single instance of the super hammer
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        deathTimer--;
        if (deathTimer <= 0)
        {
            instance = null;
            Destroy(this.gameObject);
        }
    }
}
