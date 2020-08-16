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
        base.OnInspectorGUI();
    }
}