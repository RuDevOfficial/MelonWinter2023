using System;
using UnityEngine;

public class Clover : MonoBehaviour
{
    GameObject cloverHead;

    //References
    SpriteRenderer spriteRenderer;

    //Settings
    float growthMultiplier;

    float growthThresholdTime;
    float timer;
    int growStage = 0;
    int optimalGrowthStage;

    public Action OnHarvest;

    bool growing = false;


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
        if (IsGrowing())
        {
            timer -= Time.deltaTime * growthMultiplier;
            if (timer <= 0)
            {
                GrowToNextStage();
                timer = growthThresholdTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) // Esto se borra luego
        {
            TryCut();
        }
    }

    private bool IsGrowing()
    {
        return growing == true;
    }

    private void TryCut()
    {
        if (CanCut())
        {
            Cut();
        }
    }

    private bool CanCut()
    {
        return growStage == GameManager.Get().GameData.OptimalGrowthStage;
    }

    private void GrowToNextStage()
    {
        growStage++;
        if (growStage == optimalGrowthStage)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (growStage > optimalGrowthStage)
        {
            spriteRenderer.color = Color.black;
            growing = false;
        }
    }

    public void BeginGrow(float newMultiplier)
    {
        growStage = 0;
        growing = true;
        spriteRenderer.color = Color.green;
        growthMultiplier = newMultiplier;
        SetShow(true);
    }

    private void SetShow(bool v)
    {
        spriteRenderer.enabled = v;
    }

    public void Cut()
    {
        growStage = 0;
        growing = false;
        SetShow(false);
        SpawnCloverHead();
        OnHarvest?.Invoke();
    }

    private void SpawnCloverHead()
    {
        GameObject newClover =
            Instantiate(
                GameManager.Get().GameData.cloverHead,
                this.transform.position,
                this.transform.rotation
                );

        GameManager.Get().AddCharm(newClover.GetComponent<Charm>());
    }
}
