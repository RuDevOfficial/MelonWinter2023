using UnityEngine;

public class Pot : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    float minGrowthRate;
    float maxGrowthRate;
    float growthRate;
    CloverPlant clover;

    bool unlocked = true;
    bool empty = true;

    private void Awake()
    {
        clover = GetComponentInChildren<CloverPlant>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    private void Start()
    {
        minGrowthRate = GameManager.Get().GameData.MinGrowthRate;
        maxGrowthRate = GameManager.Get().GameData.MaxGrowthRate;
    }

    private void ChangeGrowthRate()
    {
        growthRate = Random.Range(minGrowthRate, maxGrowthRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryHarvest();
        }
    }

    public void TryPlant(out bool success)
    {
        if (CanPlant())
        {
            Plant();
            success = true;
        }
        else
        {
            success = false;
        }
    }

    public void TryHarvest()
    {
        if (CanHarvest())
        {
            Harvest();
        }
    }

    private void Harvest()
    {
        empty = true;
        clover.Cut();
    }

    private bool CanHarvest()
    {
        return !empty && unlocked;
    }

    private void Plant()
    {
        empty = false;
        ChangeGrowthRate();
        clover.BeginGrow(growthRate);
    }

    bool CanPlant()
    {
        return unlocked && empty;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position, GameManager.Get().GameData.PotDetectionRadius);
        }
    }

    public void Unlock()
    {
        unlocked = true;
        spriteRenderer.color = GameManager.Get().GameData.unlockedColor;
    }

    public void Lock()
    {
        unlocked = false;
        spriteRenderer.color = GameManager.Get().GameData.lockedColor;
    }
}
