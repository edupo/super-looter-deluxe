using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionTextController : MonoBehaviour
{
    public Text text;
    public void Picked(Object obj)
    {
        var item = obj as Item;
        text.text = "Got " + item.data.description;
    }
    private void OnValidate()
    {
        if (!text)
            text = GetComponent<Text>();
    }
}
