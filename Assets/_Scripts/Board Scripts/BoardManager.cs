using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
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

        winchecker = new WinChecker(this);

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
                Board[i, j] = _board[(i * BoardWidth) + j].GetComponent<Tile>();
            }
        }

    }

    void ResetBoard()
    {

    }
}
