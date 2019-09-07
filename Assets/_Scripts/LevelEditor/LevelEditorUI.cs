using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class LevelEditorUI
{
    private EditorWindow window;
    private Board currentBoard;
    private int levelSelectedIndex = 0;

    private readonly int buttonWidth = 100;
    private readonly int buttonHeight = 30;

    public int pieceDropdownPadding = 20;
    private readonly int pieceDropDownPositionPadding = 55;


    string[] pieceOptions = { "-", "Red", "Yellow" };
    static int[,] pieceIndexMatrix; 

    // Draws the basic ui elements for loading a level, selecting a level and saving a level
    public void DrawMenu(EditorWindow _window)
    {
        window = _window;
        GUILayout.BeginArea(new Rect(20, 20, window.maxSize.x-40, 100));

            GUILayout.BeginHorizontal();

                DrawNewLevelButton();
                DrawLoadLevelMenu();
                GUILayout.Space(100);
                DrawSaveButton();
                GUILayout.Space(100);
                DrawRearrangeButtons();
                GUILayout.Space(350);
                DrawDeleteLevelButton();

            GUILayout.EndHorizontal();

        GUILayout.EndArea();

        if (currentBoard != null)
            DrawBoardEditorInterface(currentBoard);

    }

    private void DrawRearrangeButtons()
    {
        if (GUILayout.Button("Move Level Up", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            LevelDataEditor.MoveLevelUp(currentBoard.levelID);
        }

        if (GUILayout.Button("Move Level Down", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            LevelDataEditor.MoveLevelDown(currentBoard.levelID);
        }
    }

    private void DrawSaveButton()
    {
        /*if(!saveButtonEnabled)
            GUI.enabled = false; */

        if(GUILayout.Button("Save Level", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            //Debug.Log("Save clicked");
            LevelEditorDataLoader.SaveLevel(currentBoard);
        }
        GUI.enabled = true;
    }

    private void DrawLoadLevelMenu()
    {

        string[] finalList = LevelEditorDataLoader.LoadDropDownLevelList();


        levelSelectedIndex = EditorGUI.Popup(new Rect(115, 32, 80, 15),
            levelSelectedIndex,
            finalList);

        if(GUILayout.Button("Load Level", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            //Always fetch the newest level data
            UpdateBoardEditorInterfaceData();
        }
    }

    private void UpdateBoardEditorInterfaceData()
    {
        currentBoard = LevelEditorDataLoader.FetchLevelBoard(levelSelectedIndex);

        pieceIndexMatrix = new int[currentBoard.rows, currentBoard.columns];
        pieceIndexMatrix = currentBoard.BoardPiecesID;

        DrawBoardEditorInterface(currentBoard);
    }

    private void DrawNewLevelButton()
    {
        if(GUILayout.Button("New Level", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            OpenNewLevelModal();
        }
    }

    private void OpenNewLevelModal()
    {
        LevelEditorNewLevelModal modal = new LevelEditorNewLevelModal(this);
        modal.Show();
    }

    private void DrawDeleteLevelButton()
    {
        if(GUILayout.Button("Delete Level", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            if (currentBoard != null) {
                bool dialogResponse = EditorUtility.DisplayDialog("Delete Level?" + currentBoard.levelID,
                    "Are you sure you want to delete the current level? Level: " + currentBoard.levelID + "",
                    "Delete Level", "Cancel");

                if (dialogResponse)
                {
                    LevelDataEditor.DeleteLevel(currentBoard.levelID);

                    //Clear the window to remove the board editing interface for deleted board
                    currentBoard = null;
                    window.Repaint();
                }
            }
        }
    }

    private void DrawBoardEditorInterface(Board board)
    {
        int rows = board.rows;
        int columns = board.columns;

        int counter = 0;

        for (int i = rows-1; i >= 0; i--)
        {
            //We want to center the Board Editor Interface in the window
            GUILayout.BeginArea(new Rect(window.minSize.x / 2 - (columns * ((pieceDropDownPositionPadding / 2))),
                window.minSize.y / 2 - (rows * (pieceDropDownPositionPadding/2)),
                window.maxSize.x - 40, 900));
            

            GUILayout.BeginHorizontal();

            for (int j = columns-1; j >= 0; j--)
            {

                GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));
                style.focused.textColor = Color.black;
                style.active.textColor = Color.black;

                //Draws the dropdown buttons at the specified location, with the padding between each
                pieceIndexMatrix[i,j] = EditorGUI.Popup(new Rect(pieceDropDownPositionPadding * j,
                    pieceDropDownPositionPadding * i, 50, 50),
                    pieceIndexMatrix[i, j],
                    pieceOptions, style);

                //Debug.Log("i:" + i  + " j:" + j + " ori: " + board.BoardPiecesID[i, j] + " ID: " + LevelDataConverter.ToStringTypeFromID(board.BoardPiecesID[i, j]));
                GUILayout.Space(pieceDropdownPadding);
                counter++;
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        
    }


}
