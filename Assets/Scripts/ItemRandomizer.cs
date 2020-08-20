using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    public ItemGenerator generator;
    public int catergory = -1;
    public int status = -1;

    [Header("References")]
    public Item item;

    void Start()
    {
        item.data = generator.Generate(catergory, status);
        Destroy(this);
    }

    private void OnValidate()
    {
        if (!item)
            item = GetComponent<Item>();
    }
}
