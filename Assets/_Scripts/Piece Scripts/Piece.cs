using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public abstract class Piece : MonoBehaviour
{
    public enum Piece_Types
    {
        RED = 1,
        YELLOW = 2
    }

    public Piece.Piece_Types type;
    public PieceStateHandler stateHandler;

    public static float PlacedHeight = 2.8f;
    public static float HoverHeight = 4.5f;
    
    public void Setup(Piece_Types _type)
    {
        stateHandler = new PieceStateHandler(this);
        type = _type;
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
