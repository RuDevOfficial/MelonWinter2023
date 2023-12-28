using System;
using UnityEngine;

public class GloveTool: MonoBehaviour
{
    PickableObject currentObject;
    float pickUpRadius;
    private void Start()
    {
        pickUpRadius = GameManager.Get().GameData.PickUpRadius;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryPickUp();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            TryDrop();
        }
    }

    private void TryDrop()
    {
        if (CanDrop())
        {
            Drop();
        }
    }

    private void Drop()
    {
        currentObject.Dropped();
        currentObject = null;
    }

    private bool CanDrop()
    {
        return currentObject != null;
    }

    private void TryPickUp()
    {
        if (CanPickUp(out PickableObject obj))
        {
            PickUp(obj);
        }
    }

    private void PickUp(PickableObject obj)
    {
        currentObject = obj;
        currentObject.Picked();
    }

    private bool CanPickUp(out PickableObject obj)
    {
        obj = null;

        float minDistance = pickUpRadius;

        foreach (PickableObject pickable in GameManager.Get().PickableObjectsList)
        {
            Vector2 pickablePos = pickable.transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float distance = Vector2.Distance(pickablePos, mousePos);
            if (distance <= pickUpRadius && distance < minDistance)
            {
                minDistance = distance;
                obj = pickable;
            }
        }

        return obj != null;
    }

    private void OnDrawGizmos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(currentObject != null) { Gizmos.color = Color.yellow; }
        else { Gizmos.color = Color.green; }

        Gizmos.DrawWireSphere(mousePos, pickUpRadius);
    }
}
