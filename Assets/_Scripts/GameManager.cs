using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameData required for the game to work, without this the game will crash
    public GameData GameData => gameData;
    [SerializeField] GameData gameData;
    public static GameManager Get()
    {
        return instance;
    }
    static GameManager instance;

    #region ListsVariables
    
    //List of objects that can be picked, this is updated when a seed is initialized
    public List<PickableObject> PickableObjectsList => pickableObjectsList;
    private List<PickableObject> pickableObjectsList = new();

    //List of charms that are generated after a clover is cut, this list gets new charms
    //when a clover spawns the charm (Gets added to the list)
    public List<CloverHead> CharmList => cloverHeadList;
    public List<CloverHead> cloverHeadList = new();
    #endregion

    #region GameLoopVariables

    //Timer that gets initialized when the game state is "Won"
    private float winTimer = 0.0f;

    //This action is invoked when the game enters the Pending status
    public Action OnPending;

    //This action is invoked when the day begins
    public Action OnRunning;

    //This action is invoked when the player lost
    //This can happen because of:
    // 1. Time limit
    // 2. Police caught the player (WIP)
    public Action OnGameOver;

    //This action is invoked when the player presses the retry button upon loosing
    public Action OnRetry;

    //This action is invoked if the player is able to fill the box with all the charms
    //required for the night
    public Action OnWin;

    //Keeps the current state of the game loop
    public GState CurrentState => currentState;
    [SerializeField] GState currentState = GState.Pending;
    
    //Keeps the current ammount of pots that are unlocked
    //This is increased upon leaving the win state to not see the unlock happen before the transition
    int currentPotUnlockAmmount;

    //Holds the current night the player is in, used to get the charm collection threshold the Box class has
    public int CurrentNight => currentNight;
    int currentNight = 0;

    #endregion

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
        //We begin by unlocking the ammount of pots required
        currentPotUnlockAmmount = GameData.starterUnlockAmmount;
        LockPots();
        SwitchState(GState.Pending);
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
            case GState.NightWon:
                UpdateWinTimer();
                break;
        }
    }

    //When the Win state is reached we take a bit of time
    //to breathe between winning and going to the next day
    private void UpdateWinTimer()
    {
        winTimer += Time.deltaTime;
        if (winTimer >= GameData.downtimeTime)
        {
            SwitchState(GState.Pending);
            winTimer = 0.0f;
        }
    }

    public void SwitchState(GState newState)
    {
        OnLeaveState(currentState);
        currentState = newState;
        OnEnterState(newState);
    }

    private void OnLeaveState(GState oldState)
    {
        switch (oldState)
        {
            //We unlock new pots when leaving this state
            case GState.NightWon:
                UnlockNewPots();
                break;
            default:
                break;
        }
    }

    private void MoveNextNight()
    {
        currentNight++;
        Mathf.Clamp(currentNight, 0, GameData.NightsAmount);
    }
    
    private void OnEnterState(GState newState)
    {
        switch (newState)
        {
            case GState.Pending:
                    OnPending?.Invoke();
                    break;
            case GState.Running:
                    OnRunning?.Invoke();
                    break;
            case GState.GameOver:
                    OnGameOver?.Invoke();
                    break;
            case GState.NightWon:
                    MoveNextNight();
                    OnWin?.Invoke();
                    break;
        }
    }

    private void UnlockNewPots()
    {
        currentPotUnlockAmmount += GameData.unlockIncreaseCount;
        PotManager.Get().Unlock(currentPotUnlockAmmount);
    }

    public void Retry()
    {
        OnRetry?.Invoke();
        SwitchState(GState.Pending);
    }

    void LockPots()
    {
        PotManager.instance.Unlock(currentPotUnlockAmmount);
    }

    #endregion

    #region ExternalCalls
    public void AddCharm(CloverHead charmObject)
    {
        cloverHeadList.Add(charmObject);
    }
    public void RemoveCloverHead(CloverHead charm)
    {
        for (int i = cloverHeadList.Count - 1; i >= 0; i--)
        {
            if (cloverHeadList[i] == charm)
            {
                cloverHeadList.RemoveAt(i);
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

    public int GetCharmsRequired()
    {
        int ammount = GameData.charmsRequiredPerNight[currentNight];
        Debug.Log("Charms required: " + ammount);
        return ammount;
    }

    #endregion

    #region Scene Management

    public void ReturnToMenu() { SceneManager.LoadScene(0); }

    #endregion
}

public enum GState { Pending, Running, GameOver, NightWon}