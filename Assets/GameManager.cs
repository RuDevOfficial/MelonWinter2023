using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData GameData => gameData;
    [SerializeField] GameData gameData;
    public static GameManager Get()
    {
        return instance;
    }
    static GameManager instance;

    #region ListsVariables
    public List<PickableObject> PickableObjectsList => pickableObjectsList;
    private List<PickableObject> pickableObjectsList = new();
    public List<Pot> PotList => potList;
    [SerializeField] private List<Pot> potList = new();

    public List<Charm> CharmList => charmList;
    public List<Charm> charmList = new();
    #endregion

    #region GameLoopVariables

    [SerializeField] GState currentState = GState.Pending;
    
    #endregion

    int currentPotUnlockAmmount;
    int currentNight = 1;
    float nightTimer;
    public bool GameOver= false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        ShutterUI.OnOpening += SwitchState;

        currentPotUnlockAmmount = GameData.starterUnlockAmmount;
        LockPots();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGameLoop();
    }

    #region GameLoop
    private void HandleGameLoop()
    {
        switch (currentState)
        {
            case GState.Pending:
                UpdatePendingState();
                break;
            case GState.Running:
                UpdateRunningState();
                break;
            default:
                break;
        }


    }


    private void UpdatePendingState()
    {

    }

    private void UpdateRunningState()
    {
    }

    public void SwitchState(GState newState)
    {
        currentState = newState;
        OnEnterState(newState);
    }

    private void OnEnterState(GState newState)
    {
        switch (newState)
        {
            case GState.Pending:
                break;
            case GState.Running:
                LockPots();
                break;
        }
    }

    void LockPots()
    {
        for (int i = 0; i < potList.Count; i++)
        {
            if(i < currentPotUnlockAmmount) { potList[i].Lock(false); }
            else { potList[i].Lock(true); }
        }
    }
    #endregion

    #region ExternalCalls
    public void AddCharm(Charm charmObject)
    {
        Debug.Log("New charm added");
        charmList.Add(charmObject);
    }
    public void RemoveCharm(Charm charm)
    {
        for (int i = charmList.Count - 1; i >= 0; i--)
        {
            if (charmList[i] == charm)
            {
                charmList.RemoveAt(i);
            }
        }
    }
    public void AddPickable(PickableObject pickableObject)
    {
        pickableObjectsList.Add(pickableObject);
    }
    public void RemovePickable(PickableObject pickableObject)
    {
        for (int i = pickableObjectsList.Count-1; i >= 0; i--)
        {
            if (pickableObjectsList[i] == pickableObject)
            {
                pickableObjectsList.RemoveAt(i);
            }
        }
    }
    #endregion
}

public enum GState { Pending, Running, }