using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateOnWin : MonoBehaviour
{
    Animator animator;
    [SerializeField] string idleName = "Idle", moveName = "Move";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.Get().OnWin += PlayMoveAnim;
        GameManager.Get().OnPending += PlayIdleAnim;
    }

    private void OnDisable()
    {
        GameManager.Get().OnPending -= PlayIdleAnim;
        GameManager.Get().OnWin -= PlayMoveAnim;
    }

    public void PlayIdleAnim() { animator.Play(idleName); }
   public void PlayMoveAnim() { animator.Play(moveName); }
}
