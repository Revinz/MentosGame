using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour, IPickupable {

    static float PlacedHeight = 0.65f;
    static float HoverHeight = 1.5f;

    public enum Piece_States
    {
        PLACED,
        HOVERING
    }

    public enum Piece_Types
    {
        PRIMARY,
        BLOCKER
    }
    
    Piece_States pieceState;
    public Piece_States State
    {
        get { return pieceState; }
        set
        {
            pieceState = value;

            //Changes the height of the piece
            if (value == Piece_States.PLACED)
                transform.position = new Vector3(transform.position.x, PlacedHeight, transform.position.z);
            else if (value == Piece_States.HOVERING)
            {
                transform.position = new Vector3(transform.position.x, HoverHeight, transform.position.z);
            }
        } 
    }

    public Piece(Piece_Types type)
    {

    }

    public void PickUp()
    {
        Debug.Log("Picked up: " + this.name);
    }

    public void PutDown()
    {
        Debug.Log("Put down: " + this.name);
    }
}
