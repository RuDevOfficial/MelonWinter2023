using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class PickableObject : MonoBehaviour
{
    Rigidbody2D rb;
    bool isPickedUp = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        GameManager.Get().AddPickable(this);
    }

    public void Picked()
    {
        isPickedUp = true;
        rb.simulated = false;
    }

    public virtual void Dropped()
    {
        isPickedUp = false;
        rb.simulated = true;
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (isPickedUp)
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
