using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("CAMERAS")]

    //[SerializeField] private Camera MainCamera;
    //[SerializeField] private Camera SelectCamera;


    [Header("CANVASES")]

    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private GameObject ControlsCanvas;
    [SerializeField] private GameObject CreditsCanvas;
    [SerializeField] private GameObject PlayerSelectCanvas;
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] private GameObject GameCanvas;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;

    [Header("POP UP")]

    [SerializeField] private GameObject Enemy1Popup;
    [SerializeField] private GameObject Enemy2Popup;
    [SerializeField] private GameObject Enemy3Popup;
    [SerializeField] private GameObject WavePopup;
    [SerializeField] private GameObject RespawnPopup;

    [Header("BUTTONS")]

    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;



    public static bool closePlayerSelect = false;

    private void Start()
    {
        //MainCamera.enabled = true;
        //SelectCamera.enabled = false;
        hostButton.onClick.AddListener(ShowPlayerSelect);
        hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost();});
        joinButton.onClick.AddListener(ShowPlayerSelect);
        joinButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient();});
        settingsButton.onClick.AddListener(ShowSettings);
        controlsButton.onClick.AddListener(ShowControls);
        creditsButton.onClick.AddListener(ShowCredits);
        pauseButton.onClick.AddListener(ShowPause);
        continueButton.onClick.AddListener(unPause);

        ShowCanvas(MainMenuCanvas);

    }

    public void Update()
    {
        if (closePlayerSelect)
        {
            PlayerSelectCanvas.SetActive(false);
        }
    }

    private void Awake()
    {
       
    }

    public void ShowCanvas(GameObject canvas)
    {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        ControlsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        PlayerSelectCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        Enemy1Popup.SetActive(false);
        Enemy2Popup.SetActive(false);
        Enemy3Popup.SetActive(false);
        WavePopup.SetActive(false);
        RespawnPopup.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);


        canvas.SetActive(true);
    }

 

    public void ShowMainMenu()
    {
        ShowCanvas(MainMenuCanvas);
    }


    public void ShowSettings()
    {
        ShowCanvas(SettingsCanvas);
    }

    public void ShowControls()
    {
        ShowCanvas(ControlsCanvas);
    }

    public void ShowCredits()
    {
        ShowCanvas(CreditsCanvas);
    }

    public void ShowPlayerSelect()
    {
        ShowCanvas(PlayerSelectCanvas);
    }

    public void ShowGameUI()
    {
        ShowCanvas(GameCanvas);
        //MainCamera.enabled = true;
        //SelectCamera.enabled = false;
    }

    public void ShowWinScreen()
    {
        ShowCanvas(WinScreen);
    }

    public void ShowLoseScreen()
    {
        ShowCanvas(LoseScreen);
    }


    public void ShowPause()
    {
        ShowCanvas(PauseCanvas);
        PauseGame();
    }

    public void unPause()
    {
        ShowCanvas(GameCanvas);
        ResumeGame();
    }

    //CURRENTLY THESE DONT DO ANYTHING

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    //CURRENTLY THESE DONT DO ANYTHING

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        ResumeGame();
        ShowMainMenu();
    }

    public void ShowEnemy1Popup()
    {
        Enemy1Popup.SetActive(true);
        PauseGame();
        HideEnemyPopupAfterDelay(Enemy1Popup, 5f);

    }

    public void ShowEnemy2Popup()
    {
        Enemy2Popup.SetActive(true);
        PauseGame();
        HideEnemyPopupAfterDelay(Enemy2Popup, 5f);

    }

    public void ShowEnemy3Popup()
    {
        Enemy3Popup.SetActive(true);
        PauseGame();
        HideEnemyPopupAfterDelay(Enemy3Popup, 5f);
    }

    private IEnumerator HideEnemyPopupAfterDelay(GameObject enemyPopup, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyPopup.SetActive(false);
        ResumeGame();
    }

    public void ShowWavePopup()
    {

        //TODO:: DEPEND ON WAVE NUMBER
        WavePopup.SetActive(true);
        PauseGame();
        HideWavePopupAfterDelay(5f);
        
    }

    private IEnumerator HideWavePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        WavePopup.SetActive(false); // Assuming WavePopup is a GameObject reference to the wave popup
        ResumeGame();
    }

    public void ShowRespawnPopup()
    {
        RespawnPopup.SetActive(true);
        HideRespawnPopupAfterDelay(RespawnPopup, 5f);

    }


    private IEnumerator HideRespawnPopupAfterDelay(GameObject RespawnPopup, float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnPopup.SetActive(false);
    }


}
