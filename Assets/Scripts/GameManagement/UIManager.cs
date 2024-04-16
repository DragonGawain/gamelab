using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public enum PauseState
{
    ISONMAINMENU,
    ISPLAYING
}

public class UIManager : MonoBehaviour
{
    [Header("CAMERAS")]
    //[SerializeField] private Camera MainCamera;
    //[SerializeField] private Camera SelectCamera;


    [Header("CANVASES")]
    [SerializeField]
    private GameObject MainMenuCanvas;

    [SerializeField]
    private GameObject SettingsCanvas;

    [SerializeField]
    private GameObject ControlsCanvas;

    [SerializeField]
    private GameObject CreditsCanvas;

    [SerializeField]
    private GameObject PlayerSelectCanvas;

    [SerializeField]
    private GameObject HostCanvas;

    [SerializeField]
    private GameObject JoinCanvas;

    [SerializeField]
    private GameObject PauseCanvas;

    [SerializeField]
    private GameObject GameCanvas;

    [SerializeField]
    private GameObject WinScreen;

    [SerializeField]
    private GameObject LoseScreen;

    [Header("POP UP")]
    [SerializeField]
    private GameObject Enemy1Popup;

    [SerializeField]
    private GameObject Enemy2Popup;

    [SerializeField]
    private GameObject Enemy3Popup;

    [SerializeField]
    private GameObject WavePopup1;

    [SerializeField]
    private GameObject WavePopup2;

    [SerializeField]
    private GameObject WavePopup3;

    [SerializeField]
    private GameObject WavePopup4;

    [SerializeField]
    private GameObject RespawnPopup;

    [Header("BUTTONS")]
    [SerializeField]
    private Button hostButton;

    [SerializeField]
    private Button joinButton;

    [SerializeField]
    private Button joinCodeButton;

    [Header("OTHER")]
    [SerializeField]
    private SelectPlayer selectPlayer;

    [SerializeField]
    private TextMeshProUGUI hostCodeText;

    [SerializeField]
    private TMP_InputField inputField;

    public static bool closePlayerSelect = false;

    public static PauseState pauseState = PauseState.ISONMAINMENU;

    public static UIManager UISingleton;

    [SerializeField]
    AudioSource confirmAudio;

    private RelayConnect relayConnect;

    private string joinCode = null;

    private void Start()
    {
        if (UISingleton == null)
            UISingleton = this;
        if (this != UISingleton)
            Destroy(this);
        DontDestroyOnLoad(this);

        confirmAudio = GetComponent<AudioSource>();

        relayConnect = GetComponent<RelayConnect>();

        //MainCamera.enabled = true;
        //SelectCamera.enabled = false;

        // Starting the game as a host
        hostButton.onClick.AddListener(ShowHost);
        hostButton.onClick.AddListener(async () =>
        {
            // Storing the join code as a string
            joinCode = await relayConnect.StartHostWithRelay();
            Debug.Log("join code: " + joinCode);
            hostCodeText.SetText(joinCode); // updating the UI with the code
        });

        // When the player presses 'Join' button, take to join canvas
        joinButton.onClick.AddListener(ShowJoin);

        joinCodeButton.onClick.AddListener(async () =>
        {
            Debug.Log(inputField.text.ToUpper());

            bool result = false;

            try
            {
                result = await relayConnect.StartClientWithRelay(inputField.text.ToUpper());
            }
            catch (Exception)
            {
                inputField.image.color = Color.red;
                throw;
            }

            if (result)
            {
                inputField.image.color = Color.green;
                ShowPlayerSelect();
            }
        });
        // settingsButton.onClick.AddListener(ShowSettings);
        // controlsButton.onClick.AddListener(ShowControls);
        // creditsButton.onClick.AddListener(ShowCredits);
        // pauseButton.onClick.AddListener(ShowPause);
        // continueButton.onClick.AddListener(unPause);


        ShowMainMenu();
    }

    public void Update()
    {
        if (closePlayerSelect)
        {
            PlayerSelectCanvas.SetActive(false);
        }
    }

    public void ShowCanvas(GameObject canvas)
    {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        ControlsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        PlayerSelectCanvas.SetActive(false);
        HostCanvas.SetActive(false);
        JoinCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        Enemy1Popup.SetActive(false);
        Enemy2Popup.SetActive(false);
        Enemy3Popup.SetActive(false);
        WavePopup1.SetActive(false);
        WavePopup2.SetActive(false);
        WavePopup3.SetActive(false);
        WavePopup4.SetActive(false);
        RespawnPopup.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);

        canvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        ShowCanvas(MainMenuCanvas);
        pauseState = PauseState.ISONMAINMENU;
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

    public void ShowHost()
    {
        ShowCanvas(HostCanvas);
    }

    public void ShowJoin()
    {
        ShowCanvas(JoinCanvas);
    }

    public void ShowPlayerSelect()
    {
        ShowCanvas(PlayerSelectCanvas);
        selectPlayer.ResetPositions();
        // TODO:: pauseState should be set to ISPLAYING when the game actually starts, not when you've clicked the host/join button.
        // Alt: if the host/join screen has a back button, make it call a different method that also sets the pauseState
        // pauseState = PauseState.ISPLAYING;
    }

    public void ShowGameUI()
    {
        confirmAudio.Play();

        StartCoroutine(bothSelected());
        pauseState = PauseState.ISONMAINMENU;
    }

    IEnumerator bothSelected()
    {
        yield return new WaitForSeconds(2f);

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

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void BackButton()
    {
        ResumeGame();
        switch (pauseState)
        {
            case PauseState.ISONMAINMENU:
                ShowMainMenu();
                break;
            case PauseState.ISPLAYING:
                ShowPause();
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        NetworkManager.Singleton.Shutdown();
        ShowMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowEnemy1Popup()
    {
        Enemy1Popup.SetActive(true);
        PauseGame();
        StartCoroutine(HideEnemyPopupAfterDelay(Enemy1Popup, 5f));
    }

    public void ShowEnemy2Popup()
    {
        Enemy2Popup.SetActive(true);
        PauseGame();
        StartCoroutine(HideEnemyPopupAfterDelay(Enemy2Popup, 5f));
    }

    public void ShowEnemy3Popup()
    {
        Enemy3Popup.SetActive(true);
        PauseGame();
        StartCoroutine(HideEnemyPopupAfterDelay(Enemy3Popup, 5f));
    }

    private IEnumerator HideEnemyPopupAfterDelay(GameObject enemyPopup, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyPopup.SetActive(false);
        ResumeGame();
    }

    public void ShowWavePopup(int waveNb)
    {
        switch (waveNb)
        {
            case 1:
                WavePopup1.SetActive(true);
                break;
            case 2:
                WavePopup2.SetActive(true);
                break;
            case 3:
                WavePopup3.SetActive(true);
                break;
            case 4:
                WavePopup4.SetActive(true);
                break;
        }

        PauseGame();
        StartCoroutine(HideWavePopupAfterDelay(5f, waveNb));
    }

    private IEnumerator HideWavePopupAfterDelay(float delay, int waveNb)
    {
        yield return new WaitForSeconds(delay);
        // Assuming WavePopupX is a GameObject reference to the wave popup
        switch (waveNb)
        {
            case 1:
                WavePopup1.SetActive(false);
                break;
            case 2:
                WavePopup2.SetActive(false);
                break;
            case 3:
                WavePopup3.SetActive(false);
                break;
            case 4:
                WavePopup4.SetActive(false);
                break;
        }
        ResumeGame();
    }

    public void ShowRespawnPopup()
    {
        RespawnPopup.SetActive(true);
        StartCoroutine(HideRespawnPopupAfterDelay(RespawnPopup, 5f));
    }

    private IEnumerator HideRespawnPopupAfterDelay(GameObject RespawnPopup, float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnPopup.SetActive(false);
    }
}
