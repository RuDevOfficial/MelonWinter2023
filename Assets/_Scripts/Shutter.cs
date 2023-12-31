using System;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    Animator animator;
    [SerializeField] AnimationClip closeClip;

    [SerializeField] AudioClip buttonPress;
    [SerializeField] AudioClip closeShutterSFX, openShutterSFX;

    public ShutterState State => currentState;
    ShutterState currentState = ShutterState.Waiting;

    float timer = 0.0f;
    float maxTimer = 0.0f;
    float timeOffset = 2.0f;

    private void Awake() { animator = GetComponent<Animator>(); }

    private void Start()
    {
        maxTimer = closeClip.length;
        timeOffset = GameManager.Get().GameData.ShutterCloseTime;
        DependencyInjector.AddDependency<Shutter>(this);
    }

    private void Update()
    {
        if(currentState == ShutterState.Closed)
        {
            timer += Time.deltaTime;
            if(timer > maxTimer + timeOffset) 
            { 
                timer = 0.0f;
                currentState = ShutterState.Opening;
                SoundManager.Get().TryPlaySound(openShutterSFX, loop: false);
                animator.Play("Open");
            }
        }
    }

    public void TryClose()
    {
        if (CanClose())
        {
            SoundManager.Get().TryPlaySound(buttonPress, loop: false);
            BeginClose();
        }
    }

    private void BeginClose()
    {
        SoundManager.Get().TryPlaySound(closeShutterSFX, loop: false);
        animator.Play("Close");
        currentState = ShutterState.Closing;
    }

    private bool CanClose()
    {
        return currentState == ShutterState.Waiting
            && GameManager.Get().CurrentState == GState.Running;
    }

    public void SetToClosed() { currentState = ShutterState.Closed; }
    public void SetToWaiting() { currentState = ShutterState.Waiting; }

    public enum ShutterState
    {
        Waiting,
        Closing,
        Closed,
        Opening
    }
}
