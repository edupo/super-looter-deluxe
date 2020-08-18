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

public class Item : MonoBehaviour
{
    public ItemData data;

    [Header("References")]
    new public SpriteRenderer renderer;

    public UnityEvent onPicked;

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

    public void Picked()
    {
        onPicked.Invoke();
        Destroy(gameObject);
    }
}
