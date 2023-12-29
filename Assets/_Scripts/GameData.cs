using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = ("GameData"))]
public class GameData: ScriptableObject
{
    [Header("Game Progression Data")]
    public int starterUnlockAmmount;
    public int unlockIncreaseCount;
    public float nightDurationSeconds;
    public int NightsAmount;
    public float downtimeTime;

    public List<int> charmsRequiredPerNight = new();

    [Header("BoxData")]
    public float xSize;
    public float ySize;

    [Header("Clovers Data")]
    public float GrowthThreshold;
    public int OptimalGrowthStage;
    public int OptimalGrowthStageGrowthThreshold;
    public GameObject cloverHead;

    [Header("Drag Data")]
    public float PickUpRadius;

    [Header("Pot Data")]
    public Color unlockedColor;
    public Color lockedColor;
    public float PotDetectionRadius;
    public float MinGrowthRate, MaxGrowthRate;

    [Header("Shutters Data")]
    public float ShutterCloseTime;
    public float ShutterUITransitionTime;

    [Header("Debug Data")]
    public bool debugMode;
    public int debugTimerMultiplier = 2;
}
