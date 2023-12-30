using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
public class ShutterUI : MonoBehaviour
{
    public static ShutterUI instance;

    Animator animator;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] CanvasGroup gameOverButtonGroup;
    [SerializeField] Image shutterImage;

    [SerializeField] List<Sprite> daySpriteList = new();
    [SerializeField] Image numberImage;
    [SerializeField] Image dayImage;
    [SerializeField] Image BustedImage;
    [SerializeField] Image TimeOutImage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        shutterImage.enabled = true;
        numberImage.enabled = false;
        dayImage.enabled = false;
        TimeOutImage.enabled = false;
        BustedImage.enabled = false;


        if (instance == null) { instance = this; }
        else { Destroy(this); }

    }
    private void OnEnable()
    {
        DigitalWatch.OnTimerFinish += UpdateTextDisplay;

        GameManager.Get().OnPending += UpdateNightDayDisplay;
        GameManager.Get().OnPending += BeginOpening;
        GameManager.Get().OnPending += HideGameOverButtons;

        GameManager.Get().OnWin += CloseShutterGently;
        GameManager.Get().OnWin += UpdateNightDayDisplay;

        GameManager.Get().OnGameOver += CloseShutterHard;
        GameManager.Get().OnGameOver += ShowGameOverButtons;
        GameManager.Get().OnGameOver += UpdateGameOverText;

    }

    private void OnDisable()
    {
        DigitalWatch.OnTimerFinish -= UpdateTextDisplay;

        GameManager.Get().OnPending += UpdateNightDayDisplay;
        GameManager.Get().OnPending -= BeginOpening;
        GameManager.Get().OnPending -= HideGameOverButtons;

        GameManager.Get().OnWin -= CloseShutterGently;
        GameManager.Get().OnWin -= UpdateNightDayDisplay;

        GameManager.Get().OnGameOver -= CloseShutterHard;
        GameManager.Get().OnGameOver -= ShowGameOverButtons;
        GameManager.Get().OnGameOver -= UpdateGameOverText;
    }

    private void UpdateGameOverText()
    {
        Debug.Log("LOST GAME UPDATE");
        string reason = GameManager.Get().GameOverReason;
        dayImage.enabled = false;
        numberImage.enabled = false;
        TimeOutImage.enabled = false;
        BustedImage.enabled = false;
        if (reason == "Busted")
        {
            BustedImage.enabled = true;
        }
        else
        {
            TimeOutImage.enabled = true;
        }
    }

    private void Start()
    {
        BeginOpening();
    }


    void BeginOpening()
    {
        StartCoroutine(OpenWithDelay());
    }

    IEnumerator OpenWithDelay()
    {
        yield return new WaitForSeconds(GameManager.Get().GameData.ShutterUITransitionTime);
        OpenShutter();
    }

    public void OpenShutter()
    {
        animator.Play("Open");
    }

    public void CloseShutterHard()
    {
        animator.Play("CloseHard");
    }

    public void CloseShutterGently()
    {
        animator.Play("CloseGently");
    }

    public void InvokeAction()
    {
        GameManager.Get().SwitchState(GState.Running);
    }

    //Not used anymore
    public void UpdateTextDisplay(string newString)
    {
        textUI.text = newString;
    }

    void UpdateNightDayDisplay()
    {
        TimeOutImage.enabled = false;
        BustedImage.enabled = false;
        dayImage.enabled = true;
        numberImage.enabled = true;
        numberImage.sprite = daySpriteList[Math.Clamp(GameManager.Get().CurrentNight, 0 , GameManager.Get().GameData.NightsAmount-1)];
    }

    void ShowGameOverButtons()
    {
        gameOverButtonGroup.interactable = true;
        gameOverButtonGroup.alpha = 1;
    }

    void HideGameOverButtons()
    {
        gameOverButtonGroup.interactable = false;
        gameOverButtonGroup.alpha = 0;
    }
}
