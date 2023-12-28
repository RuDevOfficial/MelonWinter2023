using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    [SerializeField] Texture2D scissorsCursorTexture;
    [SerializeField] Texture2D gloveCursorTexture;

    void Start()
    {
        Vector2 cursorHotSpot = new Vector2(gloveCursorTexture.width / 2, gloveCursorTexture.height / 2);
        Cursor.SetCursor(gloveCursorTexture, cursorHotSpot, CursorMode.ForceSoftware);
    }

    void Update()
    {
        
    }
}
