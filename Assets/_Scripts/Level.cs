using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Template to create a level.


public class Level {

    public int BoardSizeX;
    public int BoardSizeY;

    public BoardTile[,] board; //The full and final board of tiles and pieces -- at the start of the level

    public Level(int sizeX, int sizeY)
    {

    }  

    public void LoadLevel()
    {
        BoardGenerator.GenerateBoard(this);
    }

    public void AddPieceAt(int x, int y, Piece.Piece_Types pieceType)
    {
        //board[x, y].piece = new Piece(Piece.Piece_Types.BLOCKER);
    }

    public void RemoveTileAt(int x, int y)
    {

    }

}
