using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemGenerator))]
public class ItemGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate"))
        {
            (target as ItemGenerator).GenerateDebug();
        }
        base.OnInspectorGUI();
    }
}
