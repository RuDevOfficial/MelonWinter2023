using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    [SerializeField] Texture2D scissorsCursorTexture;
    [SerializeField] Texture2D scissorsPressedCursorTexture;
    [SerializeField] Texture2D gloveCursorTexture;
    [SerializeField] Texture2D glovePressedCursorTexture;

    [SerializeField] Vector2 hotSpot;
    ToolsManager.ToolType currentType = ToolsManager.ToolType.Glove;

    private void OnEnable() { ToolsManager.OnToolsSwapped += OnToolsSwapped; }

    private void OnDisable() { ToolsManager.OnToolsSwapped -= OnToolsSwapped; }

    void Start()
    {
        Cursor.SetCursor(gloveCursorTexture, hotSpot, CursorMode.ForceSoftware);
    }

    private void OnToolsSwapped(ToolsManager.ToolType type)
    {
        currentType = type;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetTexture(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SetTexture(false);
        }
    }

    private void SetTexture(bool isPressed)
    {
        Texture2D texture = gloveCursorTexture;
        Vector2 pixelHotSpot = hotSpot * texture.width;
        
        if (isPressed)
        {
            if (currentType == ToolsManager.ToolType.Glove)
            {
                texture = glovePressedCursorTexture;
            }
            else
            {
                texture = scissorsPressedCursorTexture;

            }
        }
        else
        {

            if (currentType == ToolsManager.ToolType.Glove)
            {
                texture = gloveCursorTexture;
            }
            else
            {
                texture = scissorsCursorTexture;
            }
        }
        
        Cursor.SetCursor(texture, hotSpot, CursorMode.ForceSoftware);
    }
}
