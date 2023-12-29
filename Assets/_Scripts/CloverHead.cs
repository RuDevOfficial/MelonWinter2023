using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloverHead : PickableObject
{
    SpriteRenderer spriteRenderer;
    bool optimal = false;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(int stage)
    {
        spriteRenderer.sprite = GameManager.Get().GameData.CloverHeadSpritesList[stage];
        
        optimal = stage == GameManager.Get().GameData.OptimalGrowthStage;
        CanBePicked = optimal;
        
        if (!optimal) { Dropped(); }
    }

    public void Remove() 
    {
        Dropped();
        GameManager.Get().RemoveCloverHead(this);
        Destroy(this.gameObject);
    }
}