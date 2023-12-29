using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static Box instance;

    public Action OnBoxFilled; 
    private int currentCharmsCollected = 0;

    bool filled = false;

    private void OnEnable()
    {
        GameManager.Get().OnRunning += UnlockBox;
    }

    private void OnDisable()
    {
        GameManager.Get().OnRunning += UnlockBox;
    }

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    float xSize = 1.5f;
    float ySize = 2.0f;
    float yOffset = 0.5f;

    private void Update()
    {
        if (CharmCollided(out Charm charm)) { AddCharmToBox(charm); }
    }

    private void AddCharmToBox(Charm charm)
    {
        currentCharmsCollected++;
        charm.Remove();

        if (BoxJustFilled())
        {
            Debug.Log("Filled it all");
            filled = true;
            GameManager.Get().SwitchState(GState.NightWon);
        }
    }

    private bool BoxJustFilled()
    {
        return currentCharmsCollected >= GameManager.Get().GetCharmsRequired()
            && filled == false;
    }

    private bool CharmCollided(out Charm chosenCharm)
    {
        chosenCharm = null;

        if (GameManager.Get().CharmList.Count == 0) { return false; }


        foreach (Charm charm in GameManager.Get().CharmList)
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

    private void UnlockBox()
    {
        filled = false;
        currentCharmsCollected = 0;
    }
}