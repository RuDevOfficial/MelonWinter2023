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
        if (HasPotClose(out Pot closerPot))
        {
            closerPot.TryHarvest();
        }
    }
    private bool HasPotClose(out Pot closerPot)
    {
        closerPot = null;
        float minDistanceRecord = GameManager.Get().GameData.PotDetectionRadius;

        foreach (Pot pot in GameManager.Get().PotList)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 potPos = pot.transform.position;

            float distance = Vector2.Distance(mousePos, potPos);
            if (distance <= minDistanceRecord)
            {
                minDistanceRecord = distance;
                closerPot = pot;
                Debug.Log(pot.name);
            }
        }

        if (closerPot != null) { return true; }
        return false;
    }
}


