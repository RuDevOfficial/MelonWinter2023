using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holster : MonoBehaviour
{

    [SerializeField] SpriteRenderer scissorsUI;

    public void SetActiveScissorsUI()
    {
        var toolsManager = DependencyInjector.GetDependency<ToolsManager>();
        scissorsUI.enabled = toolsManager.CurrentTool.type != ToolsManager.ToolType.Scissors;
    }
}
