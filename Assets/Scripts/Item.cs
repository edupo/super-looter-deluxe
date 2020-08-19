using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ItemData
{
    public string description;
    public Color color;
    public int value;
    public Sprite sprite;
}

public class Item : Interactive
{
    public ItemData data;

    [Header("References")]
    new public SpriteRenderer renderer;

    void Start()
    {
        renderer.sprite = data.sprite;
        renderer.color = data.color;
    }

    private void OnValidate()
    {
        if (!renderer)
            renderer = GetComponentInChildren<SpriteRenderer>();
    }

    override public bool Actuate()
    {
        Destroy(gameObject);
        return true;
    }
}
