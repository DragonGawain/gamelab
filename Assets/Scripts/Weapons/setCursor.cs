using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour
{
    public Texture2D crosshair;
    public Texture2D uiCursor;
    private Vector2 cursorOffset;
    bool cursorIsUI = true;

    // Start is called before the first frame update
    void Start()
    {
        EnableUICursor();
    }

    public void EnableGameCursor()
    {
        Cursor.SetCursor(
            crosshair,
            new(crosshair.width / 2, crosshair.height / 2),
            CursorMode.Auto
        );
    }

    public void EnableUICursor()
    {
        Cursor.SetCursor(uiCursor, new(uiCursor.width / 2, uiCursor.height / 2), CursorMode.Auto);
    }

    private void Update()
    {
        switch (UIManager.pauseState)
        {
            case PauseState.ISONMAINMENU:
                if (!cursorIsUI)
                {
                    EnableUICursor();
                    cursorIsUI = true;
                }
                break;
            case PauseState.ISPLAYING:
                if (cursorIsUI)
                {
                    EnableGameCursor();
                    cursorIsUI = false;
                }
                break;
        }
    }
}
