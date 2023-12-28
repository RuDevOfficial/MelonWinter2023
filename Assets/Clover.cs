using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clover : MonoBehaviour
{
    // Start is called before the first frame update

    float growthMultiplier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BeginGrow(float mltp)
    {
        growthMultiplier = mltp;
        gameObject.SetActive(true);
    }

    public void Harvest()
    {
        gameObject.SetActive(false);
    }

}
