using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RandomCenterSliceGeneration))]
public class CenterSliceGeneration : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RandomCenterSliceGeneration sliceGeneration = (RandomCenterSliceGeneration)target;
        if (GUILayout.Button("Center slice generation"))
        {
            sliceGeneration.Generate();
        }
    }
}
