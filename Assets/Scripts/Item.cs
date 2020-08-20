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
    public AudioClip audio;
}

public class Item : Interactive
{
    public ItemData data;
    public float destroyDelay=1.0f;

    [Header("References")]
    new public SpriteRenderer renderer;
    public UnityEvent onActuated;

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
        Invoke("Destroy", destroyDelay);
        onActuated.Invoke();
        return true;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
