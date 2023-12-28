using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterUI : MonoBehaviour
{
    Animator animator;
    bool isOpen = false;

    public static Action<GState> OnOpening;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(BeginOpening());
    }

    IEnumerator BeginOpening()
    {
        yield return new WaitForSeconds(3);
        OpenShutter();
    }

    public void OpenShutter()
    {
        isOpen = true;
        animator.Play("Open");
    }

    public void CloseShutter()
    {
        isOpen = false;
        animator.Play("Close");
    }

    bool IsAnimationPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void InvokeAction()
    {
        OnOpening?.Invoke(GState.Running);
    }
}
