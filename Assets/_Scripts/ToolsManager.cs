using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolsManager : MonoBehaviour
{
    public enum ToolType { Scissors, Glove}
    public Tool CurrentTool => currentTool;
    Tool currentTool;

    GloveTool gloveTool;
    ScissorsTool scissorsTool;

    public static Action<ToolType> OnToolsSwapped;

    [SerializeField] AudioClip switchSFX;

    private void Awake()
    {
        gloveTool = GetComponentInChildren<GloveTool>();
        scissorsTool = GetComponentInChildren<ScissorsTool>();

        DependencyInjector.AddDependency<ToolsManager>(this);
    }
    private void Start()
    {
        currentTool = scissorsTool;
        SwapTool();
    }


    public void SwapTool()
    {
        if (currentTool.type == ToolType.Scissors)
        {
            scissorsTool.gameObject.SetActive(false);
            gloveTool.gameObject.SetActive(true);
            currentTool = gloveTool;
        }
        else
        {
            scissorsTool.gameObject.SetActive(true);
            gloveTool.gameObject.SetActive(false);
            currentTool = scissorsTool;
        }

        OnToolsSwapped?.Invoke(currentTool.type);
        SoundManager.Get().TryPlaySound(switchSFX, false);
    }
}
