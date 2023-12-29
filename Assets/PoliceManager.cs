using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PoliceManager : MonoBehaviour
{
    PoliceManager instance;

    float timeForPlayerToReact;

    private void Awake()
    {
        if (instance = null)
            instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        GameManager.Get().GameData
    }



}
