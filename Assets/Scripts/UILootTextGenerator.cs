using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILootTextGenerator : UIController
{
    public GameObject prefab;
    public Loot loot;

    void Start()
    {
        foreach (var item in loot.loot)
        {
            var go = Instantiate(prefab, transform);
            var text = go.GetComponent<TextMeshProUGUI>();
            var color = ColorUtility.ToHtmlStringRGB(item.color);
            text.text = $"{w}Sold <color=#{color}>{item.description} {w}for {y}{item.value}$";
        }        
    }
}
