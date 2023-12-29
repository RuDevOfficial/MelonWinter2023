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
        if (stage == GameManager.Get().GameData.OptimalGrowthStage)
        {
            optimal = true;
        }
        
        if (!optimal)
        {
            Dropped();
        }

        CanBePicked = optimal;
    }

    public void Remove() 
    {
        Dropped();
        GameManager.Get().RemoveCloverHead(this);
        Destroy(this.gameObject);
    }
}