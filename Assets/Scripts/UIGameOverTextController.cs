using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOverTextController : UIController
{
    public TextMeshProUGUI text;
    public Loot loot;

    public void Start()
    {
        text.text = $"{w}You looted {y}{loot.TotalValue}${w}!\nPress any key";
    }
    private void OnValidate()
    {
        if (!text)
            text = GetComponent<TextMeshProUGUI>();
    }
}
