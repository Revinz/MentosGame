using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public int levelID;
    public string levelName;
    public int rows;
    public int columns;
    public int[,] BoardPiecesID;
    public Piece[,] BoardPieces;
    public Tile[,] BoardTiles;
    BoardStateHandler stateHandler;
    public BoardSpawner spawner;
    public bool ValidLevelBoard = false;


    public Board(int _rows, int _columns, int _levelID)
    {
        rows = _rows;
        columns = _columns;
        levelID = _levelID;
        BoardTiles = new Tile[rows, columns];
        BoardPiecesID = new int[rows, columns];

        spawner = (BoardSpawner)ScriptableObject.CreateInstance("BoardSpawner");
        spawner.Setup(this);

        if (levelID != -1 && rows > 0 && columns > 0)
        {
            ValidLevelBoard = true;
        }
    }

}
