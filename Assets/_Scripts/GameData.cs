using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = ("GameData"))]
public class GameData: ScriptableObject
{
    [Header("Game Progression Data")]
    public int starterUnlockAmmount;
    public int unlockIncreaseCount;
    public float nightDurationSeconds;
    public int NightsAmount;
    [Header("BoxData")]
    public float xSize;
    public float ySize;

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
    public GameObject cloverHead;

    [Header("Shutters Data")]
    public float ShutterCloseTime;
}
