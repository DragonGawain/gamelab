using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    Inputs physicalInputs;
    static Vector2 mousePosition;
    static Vector2 controllerMouseInput;

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
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = physicalInputs.Player.MousePos.ReadValue<Vector2>();
        if (Application.isFocused)
        {
            controllerMouseInput = (physicalInputs.Player.MoveMouse.ReadValue<Vector2>()).normalized;
            mousePosition += controllerMouseInput;
            // mousePosition = new(
            //     mousePosition.x + controllerMouseInput.x,
            //     mousePosition.y + controllerMouseInput.y
            // );
            // // Lock mouse within window
            // if (mousePosition.x > Screen.width)
            //     mousePosition.x = Screen.width;
            // if (mousePosition.x < 0)
            //     mousePosition.x = 0;

            // if (mousePosition.y > Screen.height)
            //     mousePosition.y = Screen.height;
            // if (mousePosition.y < 0)
            //     mousePosition.y = 0;
            Mouse.current.WarpCursorPosition(mousePosition);
        }
        mousePosition = new(
            mousePosition.x - (Screen.width / 2),
            mousePosition.y - (Screen.height / 2)
        );
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
