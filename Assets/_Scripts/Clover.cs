using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Clover : MonoBehaviour
{
    //References
    SpriteRenderer spriteRenderer;

    //Settings
    float growthMultiplier;

    float growthThresholdTime;
    float timer;
    int growStage = 0;
    int optimalGrowthStage;

    public Action OnHarvest;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        growthThresholdTime = GameManager.Get().GameData.GrowthThreshold;
        optimalGrowthStage = GameManager.Get().GameData.OptimalGrowthStage;
    }
    
    void Start()
    {
        SetShow(false);
        timer = growthThresholdTime;
    }

    private void Update()
    {
        Debug.Log(timer);
        timer -= Time.deltaTime * growthMultiplier;
        if (timer<= 0)
        {
            GrowToNextStage();
            timer = growthThresholdTime;
        }
    }

    private void GrowToNextStage()
    {
        growStage++;
        Debug.Log("Stage: " + growStage);
        if (growStage > optimalGrowthStage)
        {
            spriteRenderer.color = Color.black;
        }
    }

    public void BeginGrow(float mltp)
    {
        growStage = 0;
        spriteRenderer.color = Color.green;
        growthMultiplier = mltp;
        SetShow(true);
    }

    private void SetShow(bool v)
    {
        spriteRenderer.enabled = v;
    }

    public void Cut()
    {
        SetShow(false);
        OnHarvest?.Invoke();
    }
}
