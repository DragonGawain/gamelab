using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// There should only be a single object with ColorDegradation on it. It should NOT be the same object that has GameManager though cause this SHOULD be destroyed on load.
public class ColorDegradation : MonoBehaviour
{
    // [SerializeField, Range(0, 100)]
    // float saturation = 100;
    // static Material[] mats;
    static List<Color> initColors = new();
    static List<Material> mats = new();
    static Color drained = new(0.5f, 0.5f, 0.5f);
    static int totalHP = 0;
    static int currentHP = 0;

    private void Awake()
    {
        GetMats(GameObject.FindGameObjectWithTag("House").transform);

        foreach (GameObject core in GameObject.FindGameObjectsWithTag("DreamCore"))
        {
            totalHP += core.GetComponent<DCore>().GetHealth;
        }
        currentHP = totalHP;
    }

    // Can also make it so that this script goes on a parent of everything that gets hue shifted (i.e. environment) and have the script extract all the children.
    // That way, we only need to make one call for this when the core takes damage instead of looping through all the environment objects then.

    void GetMats(Transform go)
    {
        if (go.childCount > 0)
        {
            for (int i = 0; i < go.childCount; i++)
            {
                GetMats(go.GetChild(i));
            }
        }
        else
        {
            // A single mesh can have multiple materials, so we need to get all of them
            if (!go.GetComponent<Renderer>())
                return;
            Material[] materials = go.GetComponent<Renderer>().materials;
            foreach (Material mat in materials)
            {
                mats.Add(mat);
                initColors.Add(mat.GetColor("_Color"));
            }
        }
    }

    public static void UpdateGlobalHP(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            // TODO: actually implement this
            GameManager.SetYouWin();
        }
        int i = 0;
        foreach (Material mat in mats)
        {
            mat.color = Color.Lerp(drained, initColors[i], (float)currentHP / (float)totalHP);
            i++;
        }
    }
}
