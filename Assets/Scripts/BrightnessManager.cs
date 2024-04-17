using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessVolumeText;
    [SerializeField] private Volume postProcessObject;
    private ColorAdjustments _brightnessAdjustments;

    private void Start()
    {
        postProcessObject.profile.TryGet(out _brightnessAdjustments);
 
        if (PlayerPrefs.HasKey("brightnessVolume"))
        {
            LoadBrightnessVolume();
        }
        else
        {
            SetBrightnessVolume();
        }
    }

    private void Update()
    {
        SetBrightnessVolume();
    }

    void SetBrightnessVolume()
    {
        float brightnessVolume = brightnessSlider.value;
        brightnessVolumeText.text = ((int) brightnessVolume).ToString();

        if(brightnessSlider.value < 50)
        {
            float brightness = -4.0f + (brightnessVolume/12.5f);
            _brightnessAdjustments.postExposure.Override(brightness);
        }
        if(brightnessSlider.value == 50)
        {
            _brightnessAdjustments.postExposure.Override(0.0f);
        }
        if(brightnessSlider.value > 50)
        {
            float brightness = ((brightnessVolume - 50) / 12.5f);
            _brightnessAdjustments.postExposure.Override(brightness);
        }

        PlayerPrefs.SetFloat("brightnessVolume", brightnessVolume);
    }

    void LoadBrightnessVolume()
    {
        brightnessSlider.value = PlayerPrefs.GetFloat("brightnessVolume");
        SetBrightnessVolume();
    }
}
