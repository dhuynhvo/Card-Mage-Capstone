//Worked on by Dan Huynhvo
//CS426

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Cursor_Icon : MonoBehaviour
{
    public Texture2D CursorIcon;
    private Vector2 CursorHotspot;
    void Start()    // Changes cursor from pointer to fps cursor
    {
        CursorHotspot = new Vector2(CursorIcon.width / 2, CursorIcon.height / 2);
        Cursor.SetCursor(CursorIcon, CursorHotspot, CursorMode.Auto);
    }

}
