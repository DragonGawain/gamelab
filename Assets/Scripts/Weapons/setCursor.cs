using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour
{

    public Texture2D crosshair;
    public Texture2D mouseCursor;
    private Vector2 cursorOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        enableGameCursor();
    }

    private void settingNewCursor()
    {
        // center the cursor's origin cause by default its in the top left corner
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
    }
    public void enableGameCursor()
    {
        
        settingNewCursor();
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
        
    }

    public void enableUICursor()
    {
        settingNewCursor();
        
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
    }

}
