using System;
using System.Collections.Generic;
using UnityEngine;

public class Skipper : MonoBehaviour
{
    Animator animator;
    [SerializeField] List<GameObject> objects;
    int i = 0;
    bool triggered = false;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        HideAllObjects();
        ShowObject(i);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            i += 1;

            if (i > objects.Count - 1)
            {
                if(triggered == false)
                {
                    StartFade();
                    triggered = true;
                }
            }
            else
            {
                ShowObject(i);
            }
        }
    }

    private void StartFade()
    {
        animator.SetTrigger("Fade");
    }

    private void HideAllObjects()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
    void ShowObject(int i)
    {
        objects[i].SetActive(true);
    }
}
