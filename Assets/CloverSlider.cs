using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CloverSlider : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        GameManager.Get().OnPending += ResetSlider;
        GameManager.Get().OnWin += SetMaxValue;
    }

    private void OnDisable()
    {
        GameManager.Get().OnWin -= SetMaxValue;
        GameManager.Get().OnPending -= ResetSlider;
    }
    private void Update()
    {
        if (GameManager.Get().CurrentState != GState.Running)
        {
            return;
        }
        float maxClovers = GameManager.Get().GameData.charmsRequiredPerNight[GameManager.Get().CurrentNight];
        float currentClovers = DependencyInjector.GetDependency<Box>().CurrentCloverHeadsCollected;

        float fract = Mathf.Clamp01(currentClovers / maxClovers);
        Debug.Log(fract);
        slider.value = fract;
    }
    private void SetMaxValue() => slider.value = 1;


    private void ResetSlider()
    {
        slider.value = 0;
    }

}
