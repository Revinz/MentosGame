using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    //Temporary stuff start
    Level testLevel = new Level(8, 8);

    // Use this for initialization
    void Start () {       
        //testLevel.AddPieceAt(0, 5, Piece.Piece_Types.BLOCKER);

        //GenerateBoard(testLevel);
    }

    private void Update()
    {

    }

    //Temporary stuff end

    public static void GenerateBoard(Level level)
    {
        GameObject tile = (GameObject)Resources.Load("BoardTilePrototype");
        GameObject PieceBlocker = (GameObject)Resources.Load("PiecePrototype");
        BoardTile[,] board = level.board;
        float ObjectScale = tile.transform.localScale.x;

        for (int x = 0; x < level.BoardSizeX; x++)
        {
            for (int y = 0; y < level.BoardSizeY; y++)
            {
                float offsetX = (ObjectScale * level.BoardSizeX) / 2;
                float offsetY = (ObjectScale * level.BoardSizeY) / 2;
                
                //Add tile
                Instantiate(tile, new Vector3(ObjectScale * x - offsetX, 0 , ObjectScale * y - offsetY), Quaternion.identity);

                //Add Piece if it exists
                if (level.board[x,y].piece != null)
                {
                    Instantiate(PieceBlocker);
                    level.board[x, y].piece.State = Piece.Piece_States.PLACED;
                }
            }
        }
    }
}
