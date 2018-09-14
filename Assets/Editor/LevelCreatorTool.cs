using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCreatorTool : EditorWindow {

    [MenuItem("Tools/Level Editor")]
    private static void OpenLevelEditor()
    {
        EditorWindow.GetWindow(typeof(LevelCreatorTool));
    }

    void OnGUI()
    {
        // The actual window code goes here
    }
}
