using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDegradation : MonoBehaviour
{
    [SerializeField, Range(0,100)] float saturation = 100;
    Color color;

    // Update is called once per frame
    void FixedUpdate()
    {
        color = new Color(saturation/100, saturation/100, saturation/100);
        Material[] mats = GetComponent<MeshRenderer>().materials;
        foreach (Material mat in mats)
        {
            mat.SetColor("_Color", color);
        }
    }
}
