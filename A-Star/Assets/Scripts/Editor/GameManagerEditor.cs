using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    //Adding two custom buttons to the script for pre calculating the path and pre calculating the node connections
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;

        if (GUILayout.Button("Bake Path"))
        {
            myScript.SetupAlgorithm();
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Calculate Node Connections"))
        {
            myScript.SetupNodes();
            SceneView.RepaintAll();
        }
    }
}
