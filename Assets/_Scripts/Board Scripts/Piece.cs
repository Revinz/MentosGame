using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public class Piece : MonoBehaviour
{

    public enum Piece_Types
    {
        RED,
        YELLOW
    }

    public enum Piece_States
    {
        PLACED,
        HOVERING
    }

    public static float PlacedHeight = 2.8f;
    public static float HoverHeight = 4.5f;

    //Resource variables used for loading the different pieces
    public static GameObject RedPiece;
    public static GameObject YellowPiece;

    public Piece_Types type;
    private Piece_States state = Piece_States.PLACED;

    Tile prevTile;
    public Piece_States State
    {
        get { return state; }

        set
        {
            if (value == Piece_States.HOVERING)
            {
                prevTile = GetTileBelow(this);
                //Debug.Log("Hovering: " + prevTile);    
                prevTile.piece = null;
            }

            else if (value == Piece_States.PLACED)
            {
                Tile tile = GetTileBelow(this);
                Vector3 tilePos;

                //A tile is found below the piece
                if (tile != null && tile.piece == null)
                {
                    tile.piece = this;
                    tilePos = tile.transform.position; //Make it place the piece on the new tile
                    this.gameObject.transform.position = new Vector3(tilePos.x, PlacedHeight, tilePos.z);
                }
                //No tile found below it -- move it back to the previous tile
                else
                {
                    //Debug.Log("Invalid move");
                    prevTile.piece = this;
                    tilePos = prevTile.gameObject.transform.position; //Make it place the piece on the previous tile
                    this.gameObject.transform.position = new Vector3(tilePos.x, PlacedHeight, tilePos.z);                  
                }

                this.gameObject.transform.position = new Vector3(tilePos.x, PlacedHeight, tilePos.z);

                //Win check
                if (this.type == Piece.Piece_Types.RED)
                    Camera.main.GetComponent<BoardManager>().CheckForWin(this);
            }
            
            state = value;
        }
    }
   
    public Piece(Vector3 pos, Piece_Types _type)
    {
        //Load the pieces if it isn't loaded already
        if (RedPiece == null)
            RedPiece = (GameObject)Resources.Load("PieceSecondPrototype");

        if (YellowPiece == null)
            YellowPiece = (GameObject)Resources.Load("PiecePrototype");

        //Spawn the piece at the location <<pos>>
        switch (_type)
        {
            case Piece_Types.RED:
                Instantiate(RedPiece, pos, Quaternion.identity);
                break;

            case Piece_Types.YELLOW:
                Instantiate(YellowPiece, pos, Quaternion.identity);
                break;

        }

    }

    private void Awake()
    {
        //Make the piece get connected to the tile below it
        if (runInEditMode)
        {
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, -this.transform.up);

            if (Physics.Raycast(ray, out hit, 5f, 1 << 10))
            {
                if (hit.transform.gameObject.GetComponent<Tile>() != null)
                {
                    Tile tile = hit.transform.gameObject.GetComponent<Tile>();
                    tile.piece = this;
                }
            }           

        }
    }

    //Returns the tile below the piece
    public static Tile GetTileBelow(Piece piece)
    {
        RaycastHit hit;
        Ray ray = new Ray(piece.transform.position, -piece.transform.up);

        if (Physics.Raycast(ray, out hit, 5f, 1 << 10))
        {
            if (hit.transform.gameObject.GetComponent<Tile>() != null)
            {
                Tile tile = hit.transform.gameObject.GetComponent<Tile>();
                return tile;
            }
        }

        return null;
    }
}
