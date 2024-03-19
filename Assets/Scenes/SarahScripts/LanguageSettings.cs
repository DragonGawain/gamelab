using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageSettings : MonoBehaviour
{
    public Button leftArrow;
    public Button rightArrow;
    public TextMeshProUGUI currentLanguageText;

    private string [] languages = {"English", "French", "Turkish"};

    void Start()
    {
        leftArrow.onClick.AddListener(ChangeLanguageLeft);
        rightArrow.onClick.AddListener(ChangeLanguageRight);
    }

    private void LateUpdate()
    {
        leftArrow.onClick.AddListener(ChangeLanguageLeft);
        rightArrow.onClick.AddListener(ChangeLanguageRight);
    }

    void ChangeLanguageLeft()
    {
       if(currentLanguageText.text == languages[0])
        {
            currentLanguageText.text = languages[1];
        }
       if(currentLanguageText.text == languages[1])
        {
            currentLanguageText.text = languages[2];
        }
        if (currentLanguageText.text == languages[2])
        {
            currentLanguageText.text = languages[0];
        }
    }

    void ChangeLanguageRight()
    {
        if (currentLanguageText.text == languages[0])
        {
            currentLanguageText.text = languages[2];
        }
        if (currentLanguageText.text == languages[1])
        {
            currentLanguageText.text = languages[0];
        }
        if (currentLanguageText.text == languages[2])
        {
            currentLanguageText.text = languages[1];
        }
    }

}
