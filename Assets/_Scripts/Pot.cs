using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] float minGrowthRate;
    [SerializeField] float maxGrowthRate;
    float growthRate;
    Clover clover;

    bool unlocked = true;
    bool empty = true;

    private void Start()
    {
        if (unlocked == true)
        {
            GameManager.Get().AddPot(this);
        }
    }

    private void Awake()
    {
        clover = GetComponentInChildren<Clover>();
    }

    private void ChangeGrowthRate()
    {
        growthRate = Random.Range(minGrowthRate, maxGrowthRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryPlant();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryHarvest();
        }
    }

    public void TryPlant()
    {
        if (CanPlant())
        {
            Plant();
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
        Debug.Log("Harvest!");
        clover.Cut();
    }

    private bool CanHarvest()
    {
        return !empty && unlocked;
    }

    private void Plant()
    {
        Debug.Log("BeginGrow!");
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
}
