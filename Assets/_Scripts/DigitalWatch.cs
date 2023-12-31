using System;
using UnityEngine;
using UnityEngine.UI;

public class DigitalWatch : MonoBehaviour
{
    //Number that changes sprites
    [SerializeField] Image alteredNumberImage;

    //This action happens when the watch finishes
    public static Action<string> OnTimerFinish;

    //Keeps the sprites for all required numbers
    [SerializeField] Sprite[] numberSprites;

    public static float NightTimer => time;
    static float time = 0.0f;
    float timePerNight = 60.0f;

    bool finished = false;

    private void OnEnable()
    {
        GameManager.Get().OnPending += ResetTimer;   
        GameManager.Get().OnRunning += StartTimer;   
    }
    private void OnDisable()
    {
        GameManager.Get().OnPending -= ResetTimer;   
        GameManager.Get().OnRunning -= StartTimer;   
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (CanUpdateTimer())
        {
            UpdateTimer();
            UpdateDisplay();

            if(TimerFinished())
            {
                finished = true;
                GameManager.Get().SwitchState(GState.GameOver);
                OnTimerFinish?.Invoke("You got busted!");
            }
        }
    }
    
    void ResetTimer()
    {
        time = 0.0f;
        timePerNight = GameManager.Get().GameData.DurationPerNightList[GameManager.Get().CurrentNight];
        UpdateDisplay();
    }

    void StartTimer()
    {
        finished = false;
    }

    private bool TimerFinished()
    {
        return time >= timePerNight;
    }

    private void UpdateDisplay()
    {
        alteredNumberImage.sprite = numberSprites[Math.Clamp((int)(time/(10 * timePerNight/60)), 0, 6)];
    }

    private void UpdateTimer()
    {
        time = GameManager.Get().GameData.debugMode ?
             time + Time.deltaTime* GameManager.Get().GameData.debugTimerMultiplier:
             time + Time.deltaTime;
    }

    bool CanUpdateTimer()
    {
        return time <= timePerNight && finished == false
            && GameManager.Get().CurrentState == GState.Running;
    }
}
