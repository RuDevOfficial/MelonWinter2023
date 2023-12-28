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

    public void AddPot(Pot newPot)
    {
        PotList.Add(newPot);
    }
}
