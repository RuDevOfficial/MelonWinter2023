using System;
using System.Collections.Generic;
using UnityEngine;

public class Skipper : MonoBehaviour
{
    [SerializeField] Animation animation;
    [SerializeField] List<GameObject> objects;
    int i = 0;
    bool triggered = false;
    private void Start()
    {
        HideAllObjects();
        ShowObject(i);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
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
        else if(Input.GetKeyDown(KeyCode.A))
        {
            i -= 1;
            if(i < 0) { i = 1; }

            if (i > objects.Count - 1)
            {
                if (triggered == false)
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
        animation.Play();
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
        HideAllObjects();
        objects[i].SetActive(true);
    }
}
