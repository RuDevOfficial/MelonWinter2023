using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = ("GameData"))]
public class GameData: ScriptableObject
{
    [Header("Game Progression Data")]
    public int starterUnlockAmmount;
    public int unlockIncreaseCount;
    public int NightsAmount;
    public List<float> DurationPerNightList;
    public float downtimeTime;

    public List<int> charmsRequiredPerNight = new();

    [Header("BoxData")]
    public float xSize;
    public float ySize;

    [Header("Clovers Data")]
    public float GrowthThreshold;
    public int OptimalGrowthStage;
    public int OptimalGrowthStageGrowthThreshold;
    public List<Sprite> CloverHeadSpritesList;
    public GameObject cloverHead;
    public GameObject seed;

    [Header("Drag Data")]
    public float PickUpRadius;

    [Header("Pot Data")]
    public Sprite lockedSprite;
    public float PotDetectionRadius;
    public float MinGrowthRate, MaxGrowthRate;

    [Header("Shutters Data")]
    public float ShutterCloseTime;
    public float ShutterUITransitionTime;

    [Header("Police Data")]
    public float MaxTimeToReact;
    public List<int> SpawnTimesPerNightList;
    public float MinSpawnTimeOffset;

    [Header("Debug Data")]
    public bool debugMode;
    public int debugTimerMultiplier = 2;
}
