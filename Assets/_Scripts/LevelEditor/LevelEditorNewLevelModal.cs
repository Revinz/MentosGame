using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class LevelEditorNewLevelModal : EditorWindow
{
    private static EditorWindow window;
    private string rows = "";
    private string columns = "";
    private string levelID = "";
    private string LevelName = "";
    LevelEditorUI uiWindow;

    public LevelEditorNewLevelModal(LevelEditorUI _ui)
    {
        uiWindow = _ui;
        window = GetWindow(typeof(LevelEditorNewLevelModal));
    }

    private static void OpenLevelEditor()
    {
        window = GetWindow(typeof(LevelEditorNewLevelModal));
    }

    private void OnGUI()
    {
        try
        {
            window.maxSize = new Vector2(300f, 500f);
            window.minSize = window.maxSize;

            DrawMenu();
        } catch
        {

        }

        ValidateInput();
    }

    private void DrawMenu()
    {
        GUILayout.BeginArea(new Rect(50, 50, 200, 500));

        //rows
        GUILayout.Label("Rows");
        rows = GUILayout.TextField(rows, 2);
        GUILayout.Space(50);

        //columns     
        GUILayout.Label("Columns");
        columns = GUILayout.TextField(columns, 2);
        GUILayout.Space(50);

        //id
        GUILayout.Label("Level #");
        levelID = GUILayout.TextField(levelID, 5);
        GUILayout.Space(50);

        //Level name
        GUILayout.Label("Level Name (Optional)");
        LevelName = GUILayout.TextField(LevelName);
        GUILayout.Space(50);

        //Create level button
        if (GUILayout.Button("Create Level"))
        {
            int intRows = 0;
            int intColumns = 0;
            int intLevelID = 0;
            int.TryParse(rows, out intRows);
            int.TryParse(columns, out intColumns);
            int.TryParse(levelID, out intLevelID);
      
            if (intRows > 0 && intColumns > 0 && intLevelID > 0)
                LevelDataEditor.AddLevel(intRows, intColumns, intLevelID, LevelName);

            window.Close();
        }

        GUILayout.EndArea();

    }

    private void ValidateInput()
    {
        rows = Regex.Replace(rows, "([A-Za-z])", "");
        columns = Regex.Replace(columns, "([A-Za-z])", "");
        levelID = Regex.Replace(levelID, "([A-Za-z])", "");
    }

    //Simply updates the Window GUI
    private void UpdateWindow()
    {
        this.Repaint();
    }


}
