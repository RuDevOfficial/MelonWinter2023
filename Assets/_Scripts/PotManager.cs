using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotManager : MonoBehaviour
{
    //Se encarga de desbloquear los potes mediante
    //el tiempo, por default los potes se bloquean

    public static PotManager instance;

    public List<Pot> PotList => potList;
    List<Pot> potList = new();

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }

        potList = GetComponentsInChildren<Pot>().ToList();
    }


    private void Start() { LockAll(); }
    public static PotManager Get() { return instance; }

    void LockAll() { foreach (Pot pot in potList) { pot.Lock(); } }

    public void Unlock(int fromAmmount)
    {
        for (int i = 0; i < Mathf.Clamp(fromAmmount, 0, potList.Count); i++)
        {
            potList[i].Unlock();
        }
    }

    public bool GetClosestPot(Vector2 objectPosition, out Pot closerPot)
    {
        closerPot = null;
        float minDistanceRecord = GameManager.Get().GameData.PotDetectionRadius;

        foreach (Pot pot in PotManager.Get().PotList)
        {
            Vector2 position = objectPosition;
            Vector2 potPos = (Vector2)pot.transform.position;

            float distance = Vector2.Distance(position, potPos);
            if (distance <= minDistanceRecord)
            {
                minDistanceRecord = distance;
                closerPot = pot;
            }
        }

        if (closerPot != null) { return true; }

        return false;
    }

    public void HarvestScene()
    {
        for (int i = 0; i < potList.Count; i++)
        {
            potList[i].TryHarvest();
        }
    }
}
