﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIActionTextController : UIController
{
    public TextMeshProUGUI text;
    public void Picked(Object obj)
    {
        var item = obj as Item;
        var color = ColorUtility.ToHtmlStringRGB(item.data.color);
        text.text = $"{w}Got <color=#{color}>{item.data.description} {w}sold for {y}{item.data.value}$";
    }
    private void OnValidate()
    {
        if (!text)
            text = GetComponent<TextMeshProUGUI>();
    }
}
