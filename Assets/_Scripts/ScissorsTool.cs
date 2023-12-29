using UnityEngine;

public class ScissorsTool : Tool
{
    private void Start()
    {
        type = ToolsManager.ToolType.Scissors;
    }
    protected override bool CanDoPressAction()
    {
        return true;
    }

    protected override void DoPressAction()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (PotManager.Get().GetClosestPot(mousePos, out Pot closerPot))
        {
            closerPot.TryHarvest();
        }
    }
}


