using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class PickableObject : MonoBehaviour
{
    bool isPickedUp = false;
    public bool CanBePicked = true;

    private void Start() { GameManager.Get().AddPickable(this); }

    public virtual void Picked()
    {
        isPickedUp = true;
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public virtual void Dropped()
    {
        isPickedUp = false;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void Update()
    {
        if (isPickedUp == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }   
    }

    private void OnDestroy()
    {
        GameManager.Get().RemovePickable(this);
    }
}
