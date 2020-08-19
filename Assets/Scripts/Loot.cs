using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    //[NonSerialized]
    public List<ItemData> loot;

    public void Picked(object obj)
    {
        var item = obj as ItemData;
        loot.Add(item);
    }

    public void Clean()
    {
        loot.Clear();
    }

    public int TotalValue
    {
        get
        {
            return loot.Sum(i => i.value);
        }
    }
}
