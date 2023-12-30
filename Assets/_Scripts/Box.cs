using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] AudioClip cloverAddedSXF;
    [SerializeField] AudioClip conveyorSFX;
    Animator animator;
    ParticleSystem particleSystem;

    private static Box instance;

    public Action OnBoxFilled;

    public int CurrentCloverHeadsCollected => currentCharmsCollected;
    private int currentCharmsCollected = 0;
    bool filled = false;

    //cambiar esto por sprite bounding box
    float yOffset = 0.9f;

    Bounds bounds;
    [SerializeField] SpriteRenderer sprRendBox;

    private void OnEnable() { GameManager.Get().OnRunning += ResetBox; }

    private void OnDisable() { GameManager.Get().OnRunning += ResetBox; }

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
        DependencyInjector.AddDependency<Box>(this);
        animator = GetComponentInParent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        bounds = sprRendBox.sprite.bounds;
    }

    private void Update()
    {
        if (CharmCollided(out CloverHead charm) && CanAcceptCharms()) { AddCharmToBox(charm); }
    }

    private bool CanAcceptCharms()
    {
        return GameManager.Get().CurrentState == GState.Running;
    }

    private void AddCharmToBox(CloverHead charm)
    {
        currentCharmsCollected++;
        charm.Remove();
        SoundManager.Get().TryPlaySound(cloverAddedSXF);

        if (BoxJustFilled())
        {
            filled = true;
            SoundManager.Get().TryPlaySound(conveyorSFX);
            GameManager.Get().SwitchState(GState.NightWon);
        }
        else
        {
            animator.Play("Fill");
            particleSystem.Play();
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
            if (charm.transform.position.x > this.transform.position.x - bounds.extents.x
                && charm.transform.position.x < this.transform.position.x + bounds.extents.x
                && charm.transform.position.y > this.transform.position.y - yOffset - bounds.extents.y
                && charm.transform.position.y < this.transform.position.y - yOffset + bounds.extents.y)
            {
                chosenCharm = charm;
                return true;
            }
        }

        return false;
    }

    private void ResetBox()
    {
        filled = false;
        currentCharmsCollected = 0;
    }
}