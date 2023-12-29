using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    Animator animator;

    private bool starting = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void BeginFade()
    {
        if(starting == false)
        {
            starting = true;
            animator.Play("FadeIn");
        }
    }

    public void StartGame() { SceneManager.LoadScene(1); }
    public void QuitGame() { Application.Quit(); }
}
