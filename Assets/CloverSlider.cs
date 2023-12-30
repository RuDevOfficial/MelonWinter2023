using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloverSlider : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        float maxClovers = GameManager.Get().GameData.charmsRequiredPerNight[GameManager.Get().CurrentNight];
        float currentClovers = DependencyInjector.GetDependency<Box>().CurrentCloverHeadsCollected;

        Debug.Log(currentClovers + " " + maxClovers);
        float fract = Mathf.Clamp01(currentClovers / maxClovers);
        Debug.Log(fract);
        slider.value = fract;
    }
}
