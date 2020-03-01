using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AllSlicesInitializator))]
public class SliceGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AllSlicesInitializator slicesInitializator = (AllSlicesInitializator)target;
        if (GUILayout.Button("Generate Slices"))
        {
            slicesInitializator.GenerateSlicesForEditor();
        }
        if (GUILayout.Button("Clear Slices"))
        {
            slicesInitializator.ClearSlices();
        }
    }
}
