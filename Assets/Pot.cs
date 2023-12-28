using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] float minGrowthRate;
    [SerializeField] float maxGrowthRate;
    Clover clover;

    bool unlocked = true;
    bool empty = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryPlanting();
        }
    }

    public void TryPlanting()
    {
        if (PotAviable())
        {
            Plant();
        }
    }

    private void Plant()
    {
        Debug.Log("Plant!");
        empty = false;
        clover.BeginGrow();
    }

    bool PotAviable()
    {
        return unlocked && empty;
    }
} 
