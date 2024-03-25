using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Light sceneLight;
    [SerializeField] private TextMeshProUGUI brightnessVolumeText;

    private void Update()
    {
        float brightnessVolume = sceneLight.intensity;
        brightnessVolume= brightnessSlider.value;
        brightnessVolumeText.text = ((int)(brightnessVolume)).ToString();
    }
}
