using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : PickableObject
{
    [SerializeField] List<Sprite> cloverSprites;

    SpriteRenderer spriteRenderer;
    bool optimal = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int stage)
    {
        spriteRenderer.sprite = cloverSprites[stage];
        if (stage == GameManager.Get().GameData.OptimalGrowthStage)
        {
            optimal = true;
        }

        if (!optimal)
        {
            Dropped();
            
        }
    }

    public void Remove() 
    {
        Dropped();
        GameManager.Get().RemoveCharm(this);
        Destroy(this.gameObject);
    }
}