using System;
using UnityEngine;
using UnityEngine.UI;

public class Clover : MonoBehaviour
{
    //References
    SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem magicParticles;
    [SerializeField] ParticleSystem witheredParticles;
    Animator animator;

    //Settings
    float growthMultiplier;

    float growthThresholdTime;
    float timer;
    int growStage = 0;
    int optimalGrowthStage;

    bool growing = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        growthThresholdTime = GameManager.Get().GameData.GrowthThreshold;
        optimalGrowthStage = GameManager.Get().GameData.OptimalGrowthStage;
    }

    void Start()
    {
        SetActiveClover(false);
        timer = growthThresholdTime;
    }

    private void Update()
    {
        if (IsGrowing())
        {
            timer -= Time.deltaTime * growthMultiplier;
            animator.SetBool("HalfWay", timer <= growthThresholdTime / 2); // Que el tiempo ha superado la mitad.
            if (timer <= 0)
            {
                GrowToNextStage();
                timer = growthThresholdTime;
            }
        }
    }

    private bool IsGrowing()
    {
        return growing;
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
            witheredParticles.Play();
            growing = false;
        }
    }

    public void BeginGrow(float newMultiplier)
    {
        growStage = 0;
        growing = true;
        growthMultiplier = newMultiplier;
        animator.Rebind();
        animator.Update(0.0f);
        SetActiveClover(true);
    }

    private void SetActiveClover(bool value)
    {
        spriteRenderer.enabled = value;
        animator.enabled = value;
    }

    public void Cut()
    {
        SpawnCloverHead();
        growStage = 0;
        growing = false;
        SetActiveClover(false);
    }

    private void SpawnCloverHead()
    {
        CloverHead newCloverHead =
            Instantiate(
                GameManager.Get().GameData.cloverHead,
                this.transform.position,
                this.transform.rotation
                ).GetComponent<CloverHead>();

        newCloverHead.Init(growStage);
        GameManager.Get().AddCharm(newCloverHead);
    }
}
