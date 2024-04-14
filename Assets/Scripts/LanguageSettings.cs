using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class LanguageSettings : MonoBehaviour
{
    public Button leftArrow;
    public Button rightArrow;
    public TextMeshProUGUI currentLanguageText;

    // private string[] languages = { "English", "French" };
    int localeID = 0;

    void Start()
    {
        //leftArrow.onClick.AddListener(ChangeLanguageLeft);
        //rightArrow.onClick.AddListener(ChangeLanguageRight);
    }

    private void LateUpdate()
    {
        //leftArrow.onClick.AddListener(ChangeLanguageLeft);
        //rightArrow.onClick.AddListener(ChangeLanguageRight);
    }

    public void ChangeLanguageLeft()
    {
        localeID--;
        if (localeID < 0)
            localeID = 1;
        SetLocale(localeID);
    }

    public void ChangeLanguageRight()
    {
        localeID++;
        if (localeID > 2)
            localeID = 0;
        SetLocale(localeID);
    }

    void SetLocale(int locale)
    {
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
        }
    }
}
