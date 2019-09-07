using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardStateHandler
{
    public int BoardHeight = 5;
    public int BoardWidth = 5;

    public List<GameObject> _board;
    Tile[,] Board;

    WinChecker winchecker;

    public Tile[,] BoardArray
    {
        get { return Board; }
    }

    private void Start()
    {   
        
        ConvertBoardToMultiArray();
        DragAndDropController.PlayerMovesAllowed = true;
        winchecker = new WinChecker(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBoard();
        }
    }

    public void CheckForWin(Piece piece)
    {
        winchecker.CheckForWin(piece);
    }

    //Convert 1D array ofgameobjects into a 2D array of Tile classes
    void ConvertBoardToMultiArray()
    {
        Board = new Tile[BoardHeight, BoardWidth];

        //Convert 1D array to 2D array with Tile classes
        for (int i = 0; i < BoardHeight; i++) //Rows
        {
            for (int j = 0; j < BoardWidth; j++) //Columns
            {
                if (_board[(i * BoardWidth) + j] != null)
                    Board[i, j] = _board[(i * BoardWidth) + j].GetComponent<Tile>();
            }
        }

    }

    void ResetBoard()
    {       
        Debug.Log("Resetting board");
        //Deselect the current object to avoid crash
        if (Application.isEditor)
        {
            Selection.objects = new UnityEngine.Object[0];
        }
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        DragAndDropController.PlayerMovesAllowed = true;
    }


}
