using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour {

    public Vector2 ArrayPos;
    public Piece piece; //The current piece on the tile

    public TileState state = TileState.INACTIVE; 

    public enum TileState {
        INACTIVE,
        VALID_POSITION,
        INVALID_POSITION
    }

}
