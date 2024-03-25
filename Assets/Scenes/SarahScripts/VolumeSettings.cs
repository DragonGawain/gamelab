using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer gameMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        gameMixer.SetFloat("Music Volume", Mathf.Log10(musicVolume) * 20);
        musicVolumeText.text = ((int)(musicVolume)).ToString();
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = sfxSlider.value;
        gameMixer.SetFloat("SFX Volume", Mathf.Log10(sfxVolume) * 20);
        sfxVolumeText.text = ((int)(sfxVolume)).ToString();
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }

    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume();
    }

}
