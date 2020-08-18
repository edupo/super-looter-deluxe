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

    public void Picked(UnityEngine.Object obj)
    {
        var item = obj as Item;
        loot.Add(item.data);
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
