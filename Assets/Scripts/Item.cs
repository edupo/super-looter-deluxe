﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string description;
    public Color color;
    public float value;
    public Sprite sprite;
}

public class Item : MonoBehaviour
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
}