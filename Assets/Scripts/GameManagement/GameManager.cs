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
    static Vector2 altMousePositionInput;
    static Vector2 controllerMouseInput;

    static RectTransform rTransform;

    public static GameManager GMSingleton;

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
       // Cursor.lockState = CursorLockMode.Confined;
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

        /*if (Application.isFocused)
            Cursor.lockState = CursorLockMode.Confined;*/
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
        //Debug.Log("players win!");
        //  TODO:: call this when you win to show the win screen
        //UIManager.ShowWinScreen();
    }

    public static void SetYouLose()
    {
        // Called from PlayerManager.FixedUpdate
        // Called from ColorDegradation.UpdateGlobalHP
        Debug.Log("players lose :(");
        // TODO::  call this when you lose to show the lose screen
        //UIManager.ShowLoseScreen();
    }
}
