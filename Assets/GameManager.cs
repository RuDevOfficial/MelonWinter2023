using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData GameData => gameData;
    [SerializeField] GameData gameData;

    public List<PickableObject> PickableObjectsList= new();

    public List<Pot> PotList = new();

    static GameManager instance;

    int currentPotUnlockAmmount;

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

    public static GameManager Get()
    {
        return instance;
    }
    // Update is called once per frame
    void Update()
    {
        
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

    void LockPots()
    {
        for (int i = 0; i < PotList.Count; i++)
        {
            if(i < currentPotUnlockAmmount) { PotList[i].Lock(false); }
            else { PotList[i].Lock(true); }
        }
    }
}
