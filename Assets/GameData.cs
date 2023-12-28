using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = ("GameData"))]
public class GameData: ScriptableObject
{
    [Header("Clovers Data")]
    public float GrowthThreshold;
    public int OptimalGrowthStage;

    [Header("Drag Data")]
    public float PickUpRadius;

    [Header("Pot Data")]
    public float PotDetectionRadius;
}
