using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform))]
public class PointArrayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MovingPlatform platform = (MovingPlatform) target;

        DrawDefaultInspector();

        if (GUILayout.Button("Add Point"))
        {
            platform.AddPoints();
        }

        if(GUILayout.Button("Remove Point"))
        {
            platform.RemovePoint();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

    }
}