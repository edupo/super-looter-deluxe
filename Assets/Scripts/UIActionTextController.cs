using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIActionTextController : UIController
{
    public TextMeshProUGUI text;
    public void Picked(object obj)
    {
        var item = obj as ItemData;
        var color = ColorUtility.ToHtmlStringRGB(item.color);
        text.text = $"{w}Looted <color=#{color}>{item.description} {w}worth {y}{item.value}$";
    }

    public void Stolen(object obj)
    {
        var item = obj as ItemData;
        var color = ColorUtility.ToHtmlStringRGB(item.color);
        text.text = $"{r}Stolen <color=#{color}>{item.description} {r}worth {y}{item.value}$";
    }

    private void OnValidate()
    {
        if (!text)
            text = GetComponent<TextMeshProUGUI>();
    }
}
