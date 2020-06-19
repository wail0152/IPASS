using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;

        if (GUILayout.Button("Bake Path"))
        {
            myScript.SetupAlgorithm();
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Refresh"))
        {
            myScript.SetupNodes();
            SceneView.RepaintAll();
        }
    }
}
