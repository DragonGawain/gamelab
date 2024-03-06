using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorDegradation : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    float saturation = 100;
    Material[] mats;
    List<Color> initColors = new();
    Color drained = new(0.5f, 0.5f, 0.5f);

    private void Awake()
    {
        mats = GetComponent<MeshRenderer>().materials;
        foreach (Material mat in mats)
        {
            initColors.Add(mat.GetColor("_Color"));
        }
    }

    // Update is called once per frame
    // TODO:: make this not called every update frame, but instead only get called when the saturation (core HP) value changes. 
    // Can also make it so that this script goes on a parent of everything that gets hue shifted (i.e. environment) and have the script extract all the children. 
    // That way, we only need to make one call for this when the core takes damage instead of looping through all the environment objects then. 
    void Update()
    {
        int i = 0;
        foreach (Material mat in mats)
        {
            mat.color = Color.Lerp(drained, initColors[i], saturation / 100);
            i++;
        }
    }
}
