using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ToolsManager : MonoBehaviour
{
    public enum ToolType { Scissors, Glove}
    Tool currentTool;

    GloveTool gloveTool;


    private void Start()
    {
        currentTool = gloveTool;
    }

    void Update()
    {
        
    }


}
