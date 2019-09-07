using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePiece : Piece, IDragAndDrop
{

    public void Drag(Vector3 HitLocation)
    {
        Debug.Log("Object Dragging: " + gameObject.name.ToString());
        gameObject.transform.position = new Vector3(HitLocation.x, Piece.HoverHeight, HitLocation.z);

        //Make the child objects stay at the proper location
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void Drop()
    {
        stateHandler.State = PieceStateHandler.Piece_States.PLACED;
    }

    public void Select()
    {
        stateHandler.State = PieceStateHandler.Piece_States.HOVERING;
    }
}
