using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CloverPlant : MonoBehaviour
{
    //References
    [Header("References")]
    [SerializeField] ParticleSystem witheredParticles;
    [SerializeField] ParticleSystem magicParticles;

    SpriteRenderer spriteRenderer;
    Animator animator;

    //Settings
    float growthMultiplier;
    float growthThresholdTime;
    float growthTimer;

    int growStage = 0;
    int optimalGrowthStage;

    bool growing = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        growthThresholdTime = GameManager.Get().GameData.GrowthThreshold;
        optimalGrowthStage = GameManager.Get().GameData.OptimalGrowthStage;

        SetRenderClover(false);
        growthTimer = growthThresholdTime;
    }

    //If the cloverPlant is growing, we update the growthTimer using the deltaTime and growthMultiplier (this way there is a speed offset).
    //Meanwhile, we set the HalfWay bool in the animator to true if the timear reached half the time to grow to next stage.
    //If the timer reaches 0, we grow to the next stage and reset the timer.
    private void Update()
    {
        if (growing == true)
        {
            growthTimer -= Time.deltaTime * growthMultiplier;
            animator.SetBool("HalfWay", growthTimer <= growthThresholdTime / 2);
            if (growthTimer <= 0)
            {
                GrowToNextStage();
                growthTimer = growthThresholdTime;
            }
        }
    }

    private void GrowToNextStage()
    {
        growStage++;

        animator.SetTrigger("Grow");
        if (growStage == optimalGrowthStage)
        {
            magicParticles.Play();
            growthThresholdTime = GameManager.Get().GameData.OptimalGrowthStageGrowthThreshold;
        }
        else if (growStage > optimalGrowthStage)
        {
            magicParticles.Stop();
            witheredParticles.Play();
            growing = false;
        }
    }

    public void BeginGrow(float newMultiplier)
    {
        growStage = 0;
        growing = true;
        growthMultiplier = newMultiplier;
        //Reset animator
        animator.Rebind();
        animator.Update(0.0f);
        //
        spriteRenderer.flipX = Random.value >= 0.5f;
        SetRenderClover(true);
    }

    private void SetRenderClover(bool value)
    {
        spriteRenderer.enabled = value;
        animator.enabled = value;
    }

    public void Cut()
    {
        SpawnCloverHead();
        growStage = 0;
        growing = false;
        magicParticles.Stop();
        SetRenderClover(false);
        animator.SetBool("HalfWay", false);
        growthTimer = growthThresholdTime;
    }

    private void SpawnCloverHead()
    {
        CloverHead newCloverHead =
            Instantiate(
                GameManager.Get().GameData.cloverHead,
                (this.transform.position + Vector3.up * this.spriteRenderer.bounds.size.y*0.5f),
                this.transform.rotation
                ).GetComponent<CloverHead>();

        newCloverHead.Init(growStage, spriteRenderer.flipX);
        GameManager.Get().AddCloverHead(newCloverHead);
    }
}
