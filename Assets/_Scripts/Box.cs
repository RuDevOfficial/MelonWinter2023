using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    float xSize = 1.5f;
    float ySize = 2.0f;
    float yOffset = 0.5f;

    private void Update()
    {
        if(CharmCollided(out Charm charm))
        {
            Debug.Log("Charm Removed");
            charm.Remove();
        }
    }

    private bool CharmCollided(out Charm chosenCharm)
    {
        chosenCharm = null;

        if (GameManager.Get().CharmList.Count == 0) { return false; }


        foreach(Charm charm in GameManager.Get().CharmList)
        {
            if(charm.transform.position.x > this.transform.position.x - xSize/2
                && charm.transform.position.x < this.transform.position.x + xSize/2
                && charm.transform.position.y > this.transform.position.y - yOffset - ySize/2
                && charm.transform.position.y < this.transform.position.y - yOffset + ySize/2)
            {
                chosenCharm = charm;
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube((Vector2)transform.position - Vector2.up * yOffset, new Vector2(xSize, ySize));
        }
    }
}