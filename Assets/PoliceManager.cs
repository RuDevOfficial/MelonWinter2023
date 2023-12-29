using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    PoliceManager instance;
    public enum TStates { Gone, Warning, Appear}
    TStates currentState = TStates.Gone;


    int spawnIndex;

    List<float> timesToSpawnList = new();

    private void OnEnable()
    {
        GameManager.Get().OnRunning += SetCurrentNightValues;
    }
    private void OnDisable()
    {
        GameManager.Get().OnRunning += SetCurrentNightValues;
    }

    private void Awake()
    {
        if (instance = null)
            instance = this;
        else Destroy(this);
    }

    private void Start()
    {
    }


    //Sets randomly the spawnTimes of this night in the list timesToSpawnList; 
    private void SetCurrentNightValues()
    {
        var gameData = GameManager.Get().GameData;
        spawnIndex = 0;
        timesToSpawnList = new();

        int spawnTimesNeeded = gameData.SpawnTimesPerNightList[GameManager.Get().CurrentNight];

        while (timesToSpawnList.Count < spawnTimesNeeded)
        {
            timesToSpawnList = new();
            float lastTimeSet = 0;
            float currentNightDuration = gameData.DurationPerNightList[GameManager.Get().CurrentNight];
            float spawnOffset = gameData.MinSpawnTimeOffset;
            for (int i = 0; i < spawnTimesNeeded; i++)
            {
                if (lastTimeSet + spawnOffset < currentNightDuration - spawnOffset)
                {
                    float rndTime = Random.Range(lastTimeSet + spawnOffset, currentNightDuration - spawnOffset);
                    lastTimeSet = rndTime;
                    timesToSpawnList.Add(rndTime);
                }
            }
        }
        
        //Debug Times
        Debug.Log("We have set times: " + timesToSpawnList.Count);
        for (int i = 0; i < timesToSpawnList.Count; i++)
        {
            Debug.Log("TimeToSpawn: " + timesToSpawnList[i]); 
        }
    }

    //private void ChangeState()

}
