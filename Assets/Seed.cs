using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : PickableObject
{
    public override void Dropped()
    {
        base.Dropped();

        HasPotClose();
    }

    private void HasPotClose()
    {
        foreach (Pot pot in GameManager.Get().PotList)
        {
            Vector2 pickablePos = this.transform.position;
            Vector2 potPos = pot.transform.position;

            float distance = Vector2.Distance(pickablePos, potPos);
        }
    }
}
