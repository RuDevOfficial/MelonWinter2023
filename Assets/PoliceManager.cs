using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject shutterButton;
    PoliceManager instance;
    public enum TStates { Gone, Warning, Spawn}
    TStates currentState = TStates.Gone;


    int spawnIndex = 0;
    float warningTimer;
    float spawnTimer;
    bool active;

    List<float> spawnTimesList = new();

    private void OnEnable()
    {
        GameManager.Get().OnRunning += SetCurrentNightValues;
        GameManager.Get().OnPending += ActivateButton;
    }
    private void OnDisable()
    {
        GameManager.Get().OnRunning -= SetCurrentNightValues;
        GameManager.Get().OnPending -= ActivateButton;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        text.text = currentState.ToString();
        shutterButton.SetActive(false);
    }

    private void ChangeState(TStates newState)
    {
        currentState = newState;
        Debug.Log(newState);
        text.text = currentState.ToString();
        OnEnterState(newState);
    }

    private void OnEnterState(TStates newState)
    {
        switch (newState)
        {
            case TStates.Gone:
                Debug.Log("Index spawn: " + spawnIndex);
                break;
            case TStates.Warning:
                warningTimer = GameManager.Get().GameData.WarningDuration;
                break;
            case TStates.Spawn:
                spawnTimer = GameManager.Get().GameData.ShutterCloseTime;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (!active)
        {
            return;
        }

        switch (currentState)
        {
            case TStates.Gone:
                UpdateGoneState();
                break;
            case TStates.Warning:
                UpdateWarningState();
                break;
            case TStates.Spawn:
                UpdateSpawnState();
                break;
            default:
                break;
        }
    }

    private void UpdateSpawnState()
    {
        spawnTimer -= Time.deltaTime;

        var shutter = DependencyInjector.GetDependency<Shutter>();
        if (shutter.State == Shutter.ShutterState.Waiting)
        {
            GameManager.Get().SwitchState(GState.GameOver);
            active = false;
            ChangeState(TStates.Gone);
        }
        else if (spawnTimer <= 0)
        {
            ChangeState(TStates.Gone);
        }
    }

    private void UpdateWarningState()
    {
        warningTimer -= Time.deltaTime;

        //PLAY SOUND OF WARNING

        if (warningTimer <= 0)
        {
            ChangeState(TStates.Spawn);
        }
    }

    private void UpdateGoneState()
    {
        if (spawnIndex >= spawnTimesList.Count)
        {
            active = false;
            return;
        }
        float nextSpawnTime = spawnTimesList[spawnIndex];

        if (DigitalWatch.NightTimer >= nextSpawnTime)
        {
            ChangeState(TStates.Warning);
            spawnIndex++;
        }
    }

    //Sets randomly the spawnTimes of this night in the list timesToSpawnList; 
    private void SetCurrentNightValues()
    {
        spawnIndex = 0;
        var gameData = GameManager.Get().GameData;
        spawnIndex = 0;
        spawnTimesList = new();
        active = true;
        int spawnTimesNeeded = gameData.SpawnTimesPerNightList[GameManager.Get().CurrentNight];
        if (spawnTimesNeeded == 0)
        {
            active = false;
            return;
        }
        while (spawnTimesList.Count < spawnTimesNeeded)
        {
            spawnTimesList = new();
            float lastTimeSet = 0;
            float currentNightDuration = gameData.DurationPerNightList[GameManager.Get().CurrentNight];
            float spawnOffset = gameData.MinSpawnTimeOffset;
            for (int i = 0; i < spawnTimesNeeded; i++)
            {
                if (lastTimeSet + spawnOffset < currentNightDuration - spawnOffset)
                {
                    float rndTime = Random.Range(lastTimeSet + spawnOffset, currentNightDuration - spawnOffset);
                    lastTimeSet = rndTime;
                    spawnTimesList.Add(rndTime);
                }
            }
        }
        
        //Debug Times
        Debug.Log("We have set times: " + spawnTimesList.Count);
        for (int i = 0; i < spawnTimesList.Count; i++)
        {
            Debug.Log("TimeToSpawn: " + spawnTimesList[i]); 
        }
    }
    void ActivateButton() 
    {
        shutterButton.SetActive(true);
    }

}
