using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ToggleColour : MonoBehaviour, IDeselectHandler
{
    public void OnDeselect(BaseEventData data)
    {
        Toggle toggle = GetComponent<Toggle>();
        Image img = toggle.GetComponentInChildren<Image>();
        img.color = Color.white;
    }
}
