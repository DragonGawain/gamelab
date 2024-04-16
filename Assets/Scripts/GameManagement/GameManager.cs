using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class GameManager : MonoBehaviour
{
    Inputs physicalInputs;
    static Vector2 mousePosition;
    static Vector2 mousePositionInput;
    static Vector2 controllerMouseInput;

    static RectTransform rTransform;

    public static GameManager GMSingleton;
    public static List<DCore> masterDCoreList = new();

    static UIManager uim;

    private void Awake()
    {
        if (GMSingleton == null)
            GMSingleton = this;
        if (this != GMSingleton)
            Destroy(this);
        DontDestroyOnLoad(this);
        physicalInputs = new Inputs();
        physicalInputs.Player.Enable(); // TODO:: for the record, this is a BAD idea - we do NOT want the player inptus enabled by default
        rTransform = GetComponent<RectTransform>();
        Cursor.lockState = CursorLockMode.Confined;
        // uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePositionInput = physicalInputs.Player.MousePos.ReadValue<Vector2>();
        controllerMouseInput = physicalInputs.Player.MoveMouse.ReadValue<Vector2>();

        if (controllerMouseInput.magnitude > 0)
        {
            mousePositionInput = new(rTransform.position.x, rTransform.position.y);
            if (Application.isFocused)
                Mouse.current.WarpCursorPosition(mousePositionInput);
        }

        rTransform.position = new(mousePositionInput.x, mousePositionInput.y, 0);
        mousePosition = new(
            mousePositionInput.x - (Screen.width / 2),
            mousePositionInput.y - (Screen.height / 2)
        );

        if (Application.isFocused)
            Cursor.lockState = CursorLockMode.Confined;
    }

    public static Vector2 GetMousePosition()
    {
        return mousePosition.normalized;
    }

    public static Vector3 GetMousePosition3()
    {
        return new Vector3(mousePosition.x, 0, mousePosition.y).normalized;
    }

    public static Vector2 GetMousePositionNotNormalized()
    {
        return mousePosition;
    }

    public static Vector3 GetMousePosition3NotNormalized()
    {
        return new Vector3(mousePosition.x, 0, mousePosition.y);
    }

    public static void SetYouWin()
    {
        // called from WaveManager.EnemyDied
        Debug.Log("players win!");
        uim.ShowWinScreen();
    }

    public static void SetYouLose()
    {
        // Called from PlayerManager.FixedUpdate
        // Called from ColorDegradation.UpdateGlobalHP
        Debug.Log("players lose :(");
        uim.ShowLoseScreen();
    }

    public static void MasterReset()
    {
        PlayerManager.PlayerManagerMasterReset();
        WaveManager.WaveManagerMasterReset();
        foreach (DCore core in masterDCoreList)
        {
            core.gameObject.SetActive(true);
            core.DCoreMasterReset();
        }
    }

    public static void AddToMasterDCoreList(DCore core)
    {
        masterDCoreList.Add(core);
    }

    // HOUSE IS ACTIVE FROM BEGINNING -> IT WILL NEVER **NOT** BE ACTIVE -> NO (non-pause) UI SHOULD BE AT ALL TRANSPARENT
    // First wave is kicked off (started) when both players have spawned in (event subscription)
}
