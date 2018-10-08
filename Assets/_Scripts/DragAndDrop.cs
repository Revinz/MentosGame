using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    GameObject clickedObject;
    public LayerMask WhatIsBoard = 1 << 10;
    public LayerMask WhatIsDraggable = 1 << 11;

    private Vector3 offset = Vector3.zero;

    private Vector3 PreviousTileLoc = Vector3.zero;

    private void Update()
    {
        // Left Click -- pick up the piece
        if (Input.GetMouseButtonDown(0)) //Left Click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitLoc;

            //Find piece clicked
            if (Physics.Raycast(ray, out hit, 100.0f, WhatIsDraggable))
            {
                //Debug.Log("Object clicked: " + hit.transform.parent.gameObject.name.ToString());
                hitLoc = hit.point;
                clickedObject = hit.transform.parent.gameObject;

                clickedObject.GetComponent<Piece>().State = Piece.Piece_States.HOVERING;
            }

            else
            {
                return;
            }

            //Calculate Offset
            if (Physics.Raycast(ray, out hit, 100.0f, WhatIsBoard))
            {
                offset = clickedObject.transform.position - hit.point;
            }

        }

        // Left Click Drag -- drag the piece around
        if (Input.GetMouseButton(0))    
        {
            if (clickedObject != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit; 

                if (Physics.Raycast(ray, out hit, 100.0f, WhatIsBoard))
                {
                    //Debug.Log("Object Dragging: " + clickedObject.name.ToString());
                    clickedObject.transform.position = new Vector3(hit.point.x + offset.x, Piece.HoverHeight, hit.point.z + offset.z);

                    //Make the child objects stay at the proper location
                    for (int i = 0; i < clickedObject.transform.childCount; i++)
                    {
                        clickedObject.transform.GetChild(i).transform.localPosition = new Vector3(0,0,0);
                    }
                }
            }
        }

        // Left Click Release -- put the piece down
        if (Input.GetMouseButtonUp(0))
        {
            if (clickedObject != null)
            {
                clickedObject.GetComponent<Piece>().State = Piece.Piece_States.PLACED;
            }

            //Reset values
            clickedObject = null;
            PreviousTileLoc = Vector3.zero;

        }
    }

    //Puts the piece back to the previous location
    private void PutPieceToPrevLoc(Piece piece)
    {
        piece.gameObject.transform.position = PreviousTileLoc;
        Piece.GetTileBelow(piece).piece = piece;
    }

    private void UpdateTilePieceInfo(Piece piece, bool place)
    {
        Tile tile = Piece.GetTileBelow(piece);

        if (place)
            tile.piece = piece;
        else if (!place)
            tile.piece = null;

    }

}
