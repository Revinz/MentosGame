using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataConverter
{


    public static Board ConvertToBoardFromJsonData(LevelData levelData)
    {

        if (levelData.boardData == null) {
            Debug.LogWarning("Incomplete level data!");
            return null;
        }

        Board board = new Board(levelData.rows, levelData.columns, levelData.levelID);

        for (int i = 0;  i < levelData.boardData.Length; i++)
        {
            //We want to separate the valid levels and invalid levels in the level editor
            board.BoardPiecesID[i / board.columns, i % board.columns] = levelData.boardData[i];
            //Debug.Log("i:" + i + " row: " + i % board.rows + " col: " + i / board.columns + " ID: " + board.BoardPiecesID[i / board.columns, i % board.columns]);
        }

        return board;
    }

    public static LevelData ConvertToJsonLevelDataFromBoard(Board boardData)
    {
        LevelData levelData = new LevelData(boardData.rows, boardData.columns, boardData.levelName, boardData.levelID);        

        //We want to convert it to a 1D array, since Unity's json utility can't parse 2D arrays
        int[] updatedBoard = new int[boardData.BoardPiecesID.GetLength(0) * boardData.BoardPiecesID.GetLength(1)];

        for (int i = 0; i < boardData.BoardPiecesID.GetLength(0); i++)
        {
            for (int j = 0; j < boardData.BoardPiecesID.GetLength(1); j++)
            {
                updatedBoard[i * boardData.BoardPiecesID.GetLength(1) + j] = boardData.BoardPiecesID[i, j];
            }
        }
        
        levelData.boardData = updatedBoard;

        return levelData;
    }

    public static string ToStringTypeFromID(int id)
    {
        switch (id)
        {
            case 0:
                return "Empty";

            case 1:
                return "RED";

            case 2:
                return "YELLOW";
        }

        return null;
    }

    public static Piece.Piece_Types ToPieceTypeFromID(int id)
    {
        switch (id)
        {
            case 0:
                return 0;

            case 1:
                return Piece.Piece_Types.RED;

            case 2:
                return Piece.Piece_Types.YELLOW;
        }

        return 0;
    }


}
