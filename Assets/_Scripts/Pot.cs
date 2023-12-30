using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] AudioClip plantSFX;
    [SerializeField] AudioClip cutSFX;

    [SerializeField] ParticleSystem particles;
    Sprite unlockedSprite;
    SpriteRenderer spriteRenderer;

    float minGrowthRate;
    float maxGrowthRate;
    float growthRate;
    CloverPlant clover;

    bool unlocked = false;
    bool empty = true;

    private void Awake()
    {
        clover = GetComponentInChildren<CloverPlant>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        unlockedSprite = spriteRenderer.sprite;
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
            SoundManager.Get().TryPlaySound(plantSFX);
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
            SoundManager.Get().TryPlaySound(cutSFX);
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
        particles.Play();
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
        spriteRenderer.sprite = unlockedSprite;
    }

    public void Lock()
    {
        unlocked = false;
        spriteRenderer.sprite = GameManager.Get().GameData.lockedSprite;
    }
}
