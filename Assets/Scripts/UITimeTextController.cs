using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UITimeTextController : UIController
{
    public TextMeshProUGUI text;

    private void Update()
    {
        var ttg = GameManager.instance.timeToGo;
        string color = w;
        if (ttg < 10)
            color = r;
        else if (ttg < 30)
            color = y;

        string time = TimeSpan.FromSeconds(ttg).ToString("mm':'ss':'ff");

        text.text = $"{color}{time}";
    }

    private void OnValidate()
    {
        if (!text)
            text = GetComponent<TextMeshProUGUI>();
    }
}
