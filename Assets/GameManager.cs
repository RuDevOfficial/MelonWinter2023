using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Level Values
    public bool GameOver= false;
    public GameData GameData => gameData;
    [SerializeField] GameData gameData;

    //Lists
    public List<PickableObject> PickableObjectsList= new();
    public List<Pot> PotList = new();
    public List<Charm> CharmList = new();
    //---  
    
    static GameManager instance;
    int currentPotUnlockAmmount;
    int currentNight = 1;
    float nightTimer;

    public static GameManager Get()
    {
        return instance;
    }

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
        currentPotUnlockAmmount = GameManager.Get().GameData.starterUnlockAmmount;
        LockPots();
    }

    // Update is called once per frame
    void Update()
    {
        HandleNightLoop();
    }

    #region GameLoop
    private void HandleNightLoop()
    {
        nightTimer -= Time.deltaTime;
        
        if (GameOver)
        {
            HandleLostGame();
        }
        else if (nightTimer <= 0)
        {
            HandleGoNextNight();
        }


    }

    private void HandleGoNextNight()
    {
        nightTimer = gameData.nightDurationSeconds;
        GoNextNight();
    }

    private void HandleLostGame()
    {
        //Seria hacer fade a negro y entonces resetear.
        ResetNight();
    }

    private void ResetNight()
    {
        //Aqui se tiene que setear todo a 0 en base a la noche que toque.
        currentPotUnlockAmmount = GameData.starterUnlockAmmount + currentNight * GameData.unlockIncreaseCount;
        LockPots();
    }

    private void GoNextNight()
    {

        currentNight++;
        if (currentNight>GameData.NightsAmount) // Si ya han pasado 4 noches, se acaba el juego.
        {
            EndGame();
        }
        else
        {
            ResetNight();
        }

    }


    private void EndGame()
    {
    }

    void LockPots()
    {
        for (int i = 0; i < PotList.Count; i++)
        {
            if(i < currentPotUnlockAmmount) { PotList[i].Lock(false); }
            else { PotList[i].Lock(true); }
        }
    }
    #endregion

    #region ExternalCalls
    public void AddCharm(Charm charmObject)
    {
        Debug.Log("New charm added");
        CharmList.Add(charmObject);
    }
    public void RemoveCharm(Charm charm)
    {
        for (int i = CharmList.Count - 1; i >= 0; i--)
        {
            if (CharmList[i] == charm)
            {
                CharmList.RemoveAt(i);
            }
        }
    }
    public void AddPickable(PickableObject pickableObject)
    {
        PickableObjectsList.Add(pickableObject);
    }
    public void RemovePickable(PickableObject pickableObject)
    {
        for (int i = PickableObjectsList.Count-1; i >= 0; i--)
        {
            if (PickableObjectsList[i] == pickableObject)
            {
                PickableObjectsList.RemoveAt(i);
            }
        }
    }
    #endregion
}
