using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceResourceLoader
{
    //Resource variables used for loading the different pieces
    public static GameObject RedPiece;
    public static GameObject YellowPiece;

    public void LoadPieces()
    {
        //Load the pieces if it isn't loaded already
        if (RedPiece == null)
            RedPiece = (GameObject)Resources.Load("PieceSecondPrototype");

        if (YellowPiece == null)
            YellowPiece = (GameObject)Resources.Load("PiecePrototype");

    }
}
