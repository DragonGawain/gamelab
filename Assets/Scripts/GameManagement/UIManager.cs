using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Get all canvases via SerializeField GameObjects
    [SerializeField]
    GameObject MainMenuCanvas;
    private void Awake()
    {
        // Set default canvase to MainMenu
    }

    public void SetNewLocale(TMP_Dropdown localeSelector)
    {
        int locale = localeSelector.value;
        switch (locale)
        {
            case 0:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                    0
                ];
                break;
            case 1:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                    1
                ];
                break;
            case 2:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                    2
                ];
                break;
            default:
                break;
        }
    }
}
