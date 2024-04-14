using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Inputs physicalInputs;
    static Vector2 mousePosition;

    private void Awake()
    {
        physicalInputs = new Inputs();
        physicalInputs.Player.Enable(); // TODO:: for the record, this is a BAD idea - we do NOT want the player inptus enabled by default
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = physicalInputs.Player.MousePos.ReadValue<Vector2>();
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
        Debug.Log("players win!");
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
