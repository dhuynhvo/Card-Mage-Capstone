using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Cursor_Icon : MonoBehaviour
{
    public Texture2D CursorIcon;
    private Vector2 CursorHotspot;
    void Start()
    {
        //CursorHotspot = new Vector2(CursorIcon.width / 2, CursorIcon.height / 2);
        //Cursor.SetCursor(CursorIcon, CursorHotspot, CursorMode.Auto);
    }

}
