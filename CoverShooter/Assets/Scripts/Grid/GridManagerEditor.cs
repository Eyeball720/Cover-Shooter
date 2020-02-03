using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GridManager managerScript = (GridManager)target;
        if (GUILayout.Button("Create Grid"))
        {
            managerScript.CreateGrid();
        }
    }
}
