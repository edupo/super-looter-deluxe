using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    public ItemGenerator generator;

    [Header("References")]
    public Item item;

    void Start()
    {
        item.data = generator.Generate();
        Destroy(this);
    }

    private void OnValidate()
    {
        if (!item)
            item = GetComponent<Item>();
    }
}
