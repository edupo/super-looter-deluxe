using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreTextController : UIController
{
    public TextMeshProUGUI text;
    [HideInInspector]
    public int total = 0;

    public void Start()
    {
        Print(); 
    }

    public void Picked(object obj)
    {
        var item = obj as ItemData;
        total += item.value;
        Print();
    }

    public void Stolen(object obj)
    {
        var item = obj as ItemData;
        total -= item.value;
        Print();
    }

    public void Print()
    {
        text.text = $"{y}{total}$";
    }

    private void OnValidate()
    {
        if (!text)
            text = GetComponent<TextMeshProUGUI>();
    }
}
