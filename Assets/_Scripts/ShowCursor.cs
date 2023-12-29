using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    [SerializeField] Texture2D scissorsCursorTexture;
    [SerializeField] Texture2D gloveCursorTexture;

    private void OnEnable() { ToolsManager.OnToolsSwapped += OnToolsSwapped; }

    private void OnDisable() { ToolsManager.OnToolsSwapped -= OnToolsSwapped; }

    void Start()
    {
        Vector2 cursorHotSpot = new Vector2(gloveCursorTexture.width / 2, gloveCursorTexture.height / 2);
        Cursor.SetCursor(gloveCursorTexture, cursorHotSpot, CursorMode.ForceSoftware);
    }

    private void OnToolsSwapped(ToolsManager.ToolType type)
    {
        Texture2D texture;

        if (type == ToolsManager.ToolType.Scissors) texture = scissorsCursorTexture;
        else texture = gloveCursorTexture;

        Vector2 cursorHotSpot = new Vector2(texture.width / 2, texture.height / 2);
        Cursor.SetCursor(texture, cursorHotSpot, CursorMode.ForceSoftware);
    }
}
