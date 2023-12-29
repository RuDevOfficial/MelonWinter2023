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

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (instance == null) { instance = this; }
        else { Destroy(this); }

    }

    private void OnEnable()
    {
        DigitalWatch.OnTimerFinish += UpdateTextDisplay;

        GameManager.Get().OnPending += UpdateNightDayDisplay;
        GameManager.Get().OnPending += BeginOpening;
        GameManager.Get().OnPending += HideGameOverButtons;

        GameManager.Get().OnWin += CloseShutter;
        GameManager.Get().OnWin += UpdateNightDayDisplay;

        GameManager.Get().OnGameOver += CloseShutter;
        GameManager.Get().OnGameOver += ShowGameOverButtons;
    }

    private void OnDisable()
    {
        DigitalWatch.OnTimerFinish -= UpdateTextDisplay;

        GameManager.Get().OnPending += UpdateNightDayDisplay;
        GameManager.Get().OnPending -= BeginOpening;
        GameManager.Get().OnPending -= HideGameOverButtons;

        GameManager.Get().OnWin -= CloseShutter;
        GameManager.Get().OnWin -= UpdateNightDayDisplay;

        GameManager.Get().OnGameOver -= CloseShutter;
        GameManager.Get().OnGameOver -= ShowGameOverButtons;
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

    public void CloseShutter()
    {
        animator.Play("Close");
    }

    bool IsAnimationPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void InvokeAction()
    {
        GameManager.Get().SwitchState(GState.Running);
    }

    public void UpdateTextDisplay(string newString)
    {
        textUI.text = newString;
    }

    void UpdateNightDayDisplay()
    {
        textUI.text = "Day " + (GameManager.Get().CurrentNight + 1).ToString();
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
