using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class PickableObject : MonoBehaviour
{
    protected Rigidbody2D rb;
    bool isPickedUp = false;
    public bool CanBePicked = true;
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
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public virtual void Dropped()
    {
        isPickedUp = false;
        Debug.Log(" rb is null " + rb == null);
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
