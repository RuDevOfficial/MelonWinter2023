using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterUI : MonoBehaviour
{
    Animator animator;
    bool isOpen = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TryOpen()
    {
        if(IsAnimationPlaying() == false)
        {

        }
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
}
