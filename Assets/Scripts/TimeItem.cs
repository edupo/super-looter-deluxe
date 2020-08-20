using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : Interactive
{
    public float timeBonus = 5f;

    override public bool Actuate()
    {
        GameManager.instance.timeToGo += timeBonus;
        Destroy(gameObject);
        return true;
    }
}
