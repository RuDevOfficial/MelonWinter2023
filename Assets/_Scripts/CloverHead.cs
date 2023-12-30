using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CloverHead : PickableObject
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool optimal = false;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Init(int stage, bool flipX)
    {
        spriteRenderer.sprite = GameManager.Get().GameData.CloverHeadSpritesList[stage];
        spriteRenderer.flipX = flipX;
        optimal = stage == GameManager.Get().GameData.OptimalGrowthStage;
        CanBePicked = optimal;
        
        if (!optimal) { 
            Dropped(); 
            animator.SetTrigger("Fade"); 
        }
        else
        {
            StartCoroutine(DestroyAfterTime());
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(GameManager.Get().GameData.cloverHeadDieTime);
        Dropped();
        animator.SetTrigger("Fade");
        spriteRenderer.sprite = GameManager.Get().GameData.CloverHeadSpritesList[5];
        CanBePicked = false;
    }

    public void Remove() 
    {
        Dropped();
        GameManager.Get().RemoveCloverHead(this);
        Destroy(this.gameObject);
    }
}