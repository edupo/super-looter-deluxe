﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemMod
{
    public string id;
    public List<string> prefixes;
    public List<string> sufixes;
    public float valueMultiplierMin;
    public float valueMultiplierMax;
    [ColorUsage(false)]
    public Color color;
    public AudioClip audio;

    public string RandPrefix()
    {
        if (Random.value < 0.6f || prefixes.Count == 0)
            return "";
        else
            return prefixes[Random.Range(0, prefixes.Count)];
    }

    public string RandSuffix()
    {
        if (Random.value < 0.6f || sufixes.Count == 0)
            return "";
        else
            return sufixes[Random.Range(0, sufixes.Count)];
    }
}

[System.Serializable]
public class ItemBase
{
    public string name;
    public Sprite sprite;
    public float value;
}

[CreateAssetMenu]
public class ItemGenerator : ScriptableObject
{
    [Header("Probabilities")]
    public AnimationCurve catProb;
    public AnimationCurve statusProb;

    [Header("Descriptors")]
    public List<ItemBase> itemBases;
    public List<ItemMod> categories;
    public List<ItemMod> statuses;

    public void GenerateDebug()
    {
        var item = Generate();

        Debug.Log($"'{item.description}' -> {item.value}");
    }

    public ItemData Generate(int forceCategory = -1, int forceStatus = -1)
    {
        ItemData data = new ItemData();

        var item = itemBases[Random.Range(0, itemBases.Count)];
        ItemMod cat;
        if (forceCategory >= 0)
            cat = categories[forceCategory];
        else
            cat = categories[Mathf.RoundToInt(catProb.Evaluate(Random.value) * (categories.Count - 1))];
        ItemMod status;
        if (forceCategory >= 0)
            status = statuses[forceStatus];
        else 
            status = statuses[Mathf.RoundToInt(catProb.Evaluate(Random.value) * (statuses.Count - 1))];
        var catRange = catProb.Evaluate(Random.value);
        var statusRange = statusProb.Evaluate(Random.value);


        // Color generation
        
        data.sprite = item.sprite;
        data.description = GenerateName(item, cat, status);
        data.color = GenerateColor(cat, status);
        data.value = GenerateValue(item, cat, status, catRange, statusRange);
        data.audio = cat.audio;

        return data;
    }

    public int GenerateValue(ItemBase item, ItemMod cat, ItemMod status, float catRange, float statusRange)
    {
        float cvm = cat.valueMultiplierMin + catRange * (cat.valueMultiplierMax - cat.valueMultiplierMin);
        float svm = status.valueMultiplierMin + statusRange * (status.valueMultiplierMax - status.valueMultiplierMin);
        return (int) (item.value * cvm * svm);
    }

    public Color GenerateColor(ItemMod cat, ItemMod status)
    {
        float h, s, v;
        float ch, cs, cv;
        float sh, ss, sv;

        Color.RGBToHSV(cat.color, out ch, out cs, out cv);
        Color.RGBToHSV(status.color, out sh, out ss, out sv);

        h = Mathf.Clamp01(ch);
        s = Mathf.Clamp01(cs);
        v = Mathf.Clamp01(sv);

        var c = Color.HSVToRGB(h, s, v, true);
        return c;
    }

    public string GenerateName(ItemBase item, ItemMod cat, ItemMod status)
    {
        string name = item.name;
        var cp = cat.RandPrefix();
        var sp = status.RandPrefix();
        var cs = cat.RandSuffix();
        var ss = status.RandSuffix();
        if (cp != "") name = cp + " " + name;
        if (sp != "") name = sp + " " + name;
        if (cs != "" && ss != "")
            name += $" of {cs} and {ss}";
        else if (cs != "" || ss != "")
            name += $" of {cs}{ss}";

        return name;
    }
}
