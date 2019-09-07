using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    static LevelEditorUI ui = new LevelEditorUI();
    static EditorWindow editorWindow;
       
    [MenuItem("Tools/Level Editor")]
    private static void OpenLevelEditor()
    {
        editorWindow = GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {   
        try {
            editorWindow.maxSize = new Vector2(1250f, 750f);
            editorWindow.minSize = editorWindow.maxSize;
            ui.DrawMenu(this);
        }   
        catch
        {

        }
        
    }

    //Simply updates the Window GUI
    private void UpdateWindow()
    {
        this.Repaint();
    }
}
