using System;
using UnityEngine;
using UnityEngine.UI;

public class DigitalWatch : MonoBehaviour
{
    public Image alteredNumber;

    public Sprite[] numberSprites = new Sprite[5];

    float time = 0.0f;
    float maxTime = 60.0f;
    int displayInteger = 0;

    bool finished = false;

    public static Action OnFinish;

    private void Update()
    {
        if (CanUpdateTimer())
        {
            UpdateTimer();
            UpdateDisplay();

            if(TimerFinished())
            {
                finished = true;
                OnFinish?.Invoke();
            }
        }
    }

    private bool TimerFinished()
    {
        return time > maxTime;
    }

    private void UpdateDisplay()
    {
        alteredNumber.sprite = numberSprites[Math.Clamp((int)time / 10, 0, 6)];
    }

    private void UpdateTimer()
    {
        time += Time.deltaTime;
    }

    bool CanUpdateTimer()
    {
        return time <= maxTime && finished == false;
    }
}
