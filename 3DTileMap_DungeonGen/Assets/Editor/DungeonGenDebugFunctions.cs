using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonGenController))]
public class DungeonGenDebugFunctions : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DungeonGenController _dgController = (DungeonGenController)target;
        if (GUILayout.Button("StartGenerating!") && Application.isPlaying)
        {
            _dgController.StartDungeonGen();
        }
    }
}
