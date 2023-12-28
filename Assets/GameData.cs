using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = ("GameData"))]
public class GameData: ScriptableObject
{
    [Header("Game Progression Data")]
    public int starterUnlockAmmount;
    public int unlockIncreaseCount;

    [Header("Clovers Data")]
    public float GrowthThreshold;
    public int OptimalGrowthStage;

    [Header("Drag Data")]
    public float PickUpRadius;

    [Header("Pot Data")]
    public Color unlockedColor;
    public Color lockedColor;
    public float PotDetectionRadius;
    public float MinGrowthRate, MaxGrowthRate;
}
