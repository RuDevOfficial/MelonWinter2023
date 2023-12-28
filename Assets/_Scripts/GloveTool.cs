using System;
using UnityEditor.EditorTools;
using UnityEngine;

public class GloveTool: Tool
{
    PickableObject currentObject;
    float pickUpRadius;
    private void Start()
    {
        pickUpRadius = GameManager.Get().GameData.PickUpRadius;
        type = ToolsManager.ToolType.Glove;
    }
    protected override void DoReleaseAction()
    {
        currentObject.Dropped();
        currentObject = null;
    }

    protected override bool CanDoReleaseAction()
    {
        return currentObject != null;
    }

    protected override void DoPressAction()
    {
        currentObject.Picked();
    }

    protected override bool CanDoPressAction()
    {
        float minDistance = pickUpRadius;

        foreach (PickableObject pickable in GameManager.Get().PickableObjectsList)
        {
            Vector2 pickablePos = pickable.transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float distance = Vector2.Distance(pickablePos, mousePos);
            if (distance <= pickUpRadius && distance < minDistance)
            {
                minDistance = distance;
                currentObject = pickable;
            }
        }

        return currentObject != null;
    }

    private void OnDrawGizmos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(currentObject != null) { Gizmos.color = Color.yellow; }
        else { Gizmos.color = Color.green; }

        Gizmos.DrawWireSphere(mousePos, pickUpRadius);
    }
}


