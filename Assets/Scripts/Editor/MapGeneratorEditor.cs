using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate"))
        {
            (target as MapGenerator).Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            (target as MapGenerator).Clear();
        }
        base.OnInspectorGUI();
    }
}