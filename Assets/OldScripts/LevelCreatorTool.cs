/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelCreatorTool : EditorWindow {

    
    [MenuItem("Tools/Level Editor (OLD)")]
    private static void OpenLevelEditor()
    {
        EditorWindow.GetWindow(typeof(LevelCreatorTool));
    }

    //Edit board values
    int boardSizeX = 0;
    int boardSizeY = 0;

    bool EditBoardSelected = false;
    string EditBoardStatus = "Edit the board size";

    bool tileSelected = false;
    string tileStatus = "Tile Editor";
    Tile selectedTile;

    Piece.Piece_Types type = Piece.Piece_Types.YELLOW;

    void OnGUI()
    {
        EditorGUILayout.Foldout(EditBoardSelected, EditBoardStatus);
        if (EditBoardSelected)
        {
            GUILayout.Label("Board Size", EditorStyles.boldLabel);
            boardSizeX = EditorGUILayout.IntField("Width", boardSizeX);
            boardSizeY = EditorGUILayout.IntField("Height", boardSizeY);

                if (GUILayout.Button("Create new Board"))
                {                  
                    GenerateBoard(boardSizeX, boardSizeY);
                }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.Foldout(tileSelected, tileStatus);
        if (tileSelected)
        {

            type = (Piece.Piece_Types)EditorGUILayout.EnumPopup("Select Type", type);

            if (GUILayout.Button("Add tile"))
            {
                Vector3 tilePos = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y + Piece.PlacedHeight, selectedTile.transform.position.z);

                Piece piece = new MoveablePiece(tilePos, type);

                Debug.Log(selectedTile.piece);
                SetSceneDirty();
            }

            if (GUILayout.Button("Remove tile"))
            {
                DestroyImmediate(selectedTile.piece.gameObject);
                selectedTile.piece = null;
                SetSceneDirty();
            }
        }

    }

    private void UpdateWindow()
    {
        this.Repaint();
    }

    private void OnSelectionChange()
    {
        if (Selection.activeGameObject == null)
            return;

        if (Selection.activeGameObject == Camera.main.gameObject)
        {
            EditBoardSelected = true;
        } else
        {
            EditBoardSelected = false;
        }

        if (Selection.activeGameObject.GetComponent<Tile>() != null)
        {
            selectedTile = Selection.activeGameObject.GetComponent<Tile>();
            tileSelected = true;
        } else
        {
            tileSelected = false;
            selectedTile = null;
        }


    }

    public void OnInspectorUpdate()
    {
        this.Repaint();
    }

    public static void GenerateBoard(int width, int height)
    {
        GameObject tile = (GameObject)Resources.Load("BoardTilePrototype");
        float ObjectScale = tile.transform.localScale.x;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float offsetX = (ObjectScale * width) / 2;
                float offsetY = (ObjectScale * height) / 2;

                //Add tile
                Instantiate(tile, new Vector3(ObjectScale * x - offsetX, 0, ObjectScale * y - offsetY), Quaternion.identity);
            }
        }

        //Update the board height and width in the boardmanager for the scene
        BoardManager m = Camera.main.GetComponent<BoardManager>();
        m.BoardHeight = height;
        m.BoardWidth = width;
    }

    //Makes the scene save-able
    void SetSceneDirty()
    {
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
    
}

*/