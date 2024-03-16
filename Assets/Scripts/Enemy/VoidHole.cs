using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class VoidHole : MonoBehaviour
{
    public static event Action<Transform> DespawnVoidHole;
    void Start()
    {
        Invoke(nameof(DestroySelf), Random.Range(3, 7));
    }

    private void DestroySelf()
    {
        DespawnVoidHole?.Invoke(transform.parent);
        Destroy(this.gameObject);
    }
}
