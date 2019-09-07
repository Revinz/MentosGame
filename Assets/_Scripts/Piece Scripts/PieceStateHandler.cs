using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceStateHandler
{

    public enum Piece_States
    {
        PLACED,
        HOVERING
    }

    private Piece_States state = Piece_States.PLACED;
    Piece piece;

    Tile prevTile;
    public Piece_States State
    {
        get { return state; }

        set
        {
            if (value == Piece_States.HOVERING)
            {
                prevTile = Piece.GetTileBelow(piece);
                //Debug.Log("Hovering: " + prevTile);    
                prevTile.piece = null;
            }

            else if (value == Piece_States.PLACED)
            {
                Tile tile = Piece.GetTileBelow(piece);
                Vector3 tilePos;

                //A tile is found below the piece
                if (tile != null && tile.piece == null)
                {
                    tile.piece = piece;
                    tilePos = tile.transform.position; //Make it place the piece on the new tile
                    piece.gameObject.transform.position = new Vector3(tilePos.x, Piece.PlacedHeight, tilePos.z);
                }
                //No tile found below it -- move it back to the previous tile
                else
                {
                    //Debug.Log("Invalid move");
                    prevTile.piece = piece;
                    tilePos = prevTile.gameObject.transform.position; //Make it place the piece on the previous tile
                    piece.gameObject.transform.position = new Vector3(tilePos.x, Piece.PlacedHeight, tilePos.z);
                }

                piece.gameObject.transform.position = new Vector3(tilePos.x, Piece.PlacedHeight, tilePos.z);

                //Win check
                if (piece.type == Piece.Piece_Types.RED)
                    Camera.main.GetComponent<BoardStateHandler>().CheckForWin(piece);
            }

            state = value;
        }
    }


    public PieceStateHandler(Piece _Piece)
    {
        piece = _Piece;
    }
}
