using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject HostCanvas;
    [SerializeField] private GameObject JoinCanvas;
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private GameObject ControlsCanvas;
    [SerializeField] private GameObject CreditsCanvas;
    [SerializeField] private GameObject PlayerSelectCanvas;
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] private GameObject GameCanvas;
    [SerializeField] private GameObject Enemy1Popup;
    [SerializeField] private GameObject Enemy2Popup;
    [SerializeField] private GameObject Enemy3Popup;
    [SerializeField] private GameObject WavePopup;
    [SerializeField] private GameObject RespawnPopup;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;

    
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton1;
    [SerializeField] private Button backButton2;
    [SerializeField] private Button backButton3;
    [SerializeField] private Button backButton4;
    [SerializeField] private Button backButton5;



    private void Start()
    {
        hostButton.onClick.AddListener(ShowHost);
        joinButton.onClick.AddListener(ShowJoin);
        settingsButton.onClick.AddListener(ShowSettings);
        controlsButton.onClick.AddListener(ShowControls);
        creditsButton.onClick.AddListener(ShowCredits);
        backButton1.onClick.AddListener(BackToMenu);
        backButton2.onClick.AddListener(BackToMenu);
        backButton3.onClick.AddListener(BackToMenu);
        backButton4.onClick.AddListener(BackToMenu);
        backButton5.onClick.AddListener(BackToMenu);

    }


    private void Awake()
    {
        ShowCanvas(MainMenuCanvas);
    }

    public void ShowCanvas(GameObject canvas)
    {
        MainMenuCanvas.SetActive(false);
        HostCanvas.SetActive(false);
        JoinCanvas.SetActive(false);
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

    public void ShowHost()
    {
        ShowCanvas(HostCanvas);
    }

    public void ShowJoin()
    {
        ShowCanvas(JoinCanvas);
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
    }

    public void ShowWinScreen()
    {
        ShowCanvas(WinScreen);
    }

    public void ShowLoseScreen()
    {
        ShowCanvas(LoseScreen);
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

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
        HideEnemyPopupAfterDelay(Enemy1Popup, 5f);

    }

    public void ShowEnemy2Popup()
    {
        Enemy2Popup.SetActive(true);
        HideEnemyPopupAfterDelay(Enemy2Popup, 5f);

    }

    public void ShowEnemy3Popup()
    {
        Enemy3Popup.SetActive(true);
        HideEnemyPopupAfterDelay(Enemy3Popup, 5f);
    }

    private IEnumerator HideEnemyPopupAfterDelay(GameObject enemyPopup, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyPopup.SetActive(false);
    }

    public void ShowWavePopup()
    {
        WavePopup.SetActive(true);
        HideWavePopupAfterDelay(5f);

    }

    private IEnumerator HideWavePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        WavePopup.SetActive(false); // Assuming WavePopup is a GameObject reference to the wave popup
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
