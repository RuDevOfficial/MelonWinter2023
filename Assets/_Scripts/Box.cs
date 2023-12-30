using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    private static Box instance;

    public Action OnBoxFilled;

    public int CurrentCloverHeadsCollected => currentCharmsCollected;
    private int currentCharmsCollected = 0;
    bool filled = false;

    //cambiar esto por sprite bounding box
    float xSize = 1.5f;
    float ySize = 2.0f;
    float yOffset = 0.5f;

    private void OnEnable() { GameManager.Get().OnRunning += ResetBox; }

    private void OnDisable() { GameManager.Get().OnRunning += ResetBox; }

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
        DependencyInjector.AddDependency<Box>(this);
    }

    
    private void Update()
    {
        if (CharmCollided(out CloverHead charm)) { AddCharmToBox(charm); }
    }

    private void AddCharmToBox(CloverHead charm)
    {
        currentCharmsCollected++;
        charm.Remove();

        if (BoxJustFilled())
        {
            filled = true;
            GameManager.Get().SwitchState(GState.NightWon);
        }
    }

    private bool BoxJustFilled()
    {
        return currentCharmsCollected >= GameManager.Get().GetCharmsRequired()
            && filled == false;
    }

    private bool CharmCollided(out CloverHead chosenCharm)
    {
        chosenCharm = null;

        if (GameManager.Get().CharmList.Count == 0) { return false; }


        foreach (CloverHead charm in GameManager.Get().CharmList)
        {
            if (charm.transform.position.x > this.transform.position.x - xSize / 2
                && charm.transform.position.x < this.transform.position.x + xSize / 2
                && charm.transform.position.y > this.transform.position.y - yOffset - ySize / 2
                && charm.transform.position.y < this.transform.position.y - yOffset + ySize / 2)
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

    private void ResetBox()
    {
        filled = false;
        currentCharmsCollected = 0;
    }
}