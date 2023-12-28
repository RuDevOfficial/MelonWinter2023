using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : PickableObject
{
    public void Remove() 
    {
        Dropped();
        GameManager.Get().RemoveCharm(this);
        Destroy(this.gameObject);
    }
}