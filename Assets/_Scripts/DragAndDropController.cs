﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropController : MonoBehaviour {

    GameObject clickedObject;
    public LayerMask WhatIsBoard = 1 << 10;
    public LayerMask WhatIsDraggable = 1 << 11;

    private Vector3 offset = Vector3.zero;
    private IDragAndDrop IDragAndDropComponent;
    private Vector3 PreviousTileLoc = Vector3.zero;

    public static bool PlayerMovesAllowed = true;

    private void Update()
    {
        // Left Click -- pick up the piece
        if (Input.GetMouseButtonDown(0)) //Left Click
        {
            Debug.Log("Left Click clicked");

            if (!PlayerMovesAllowed)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitLoc;

            //Find piece clicked
            if (Physics.Raycast(ray, out hit, 1000.0f, WhatIsDraggable))
            {
                Debug.Log("Piece hit");
                //Debug.Log("Object clicked: " + hit.transform.parent.gameObject.name.ToString());

                hitLoc = hit.point;
                clickedObject = hit.transform.parent.gameObject;

                IDragAndDropComponent = clickedObject.GetComponent<IDragAndDrop>();
                if (IDragAndDropComponent != null)
                {
                    IDragAndDropComponent.Select(); 
                }
                
            }

            else
            {
                Debug.Log("nothing hit");
                ResetClickedObject();
                return;
            }

        }

        // Left Click Drag -- drag the piece around
        if (Input.GetMouseButton(0))        
        {
            if (clickedObject != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit; 
                Debug.DrawRay(Input.mousePosition, ray.direction, Color.red, 0.5f);


                if (Physics.Raycast(ray, out hit, 100.0f, WhatIsBoard))
                {

                    if (IDragAndDropComponent != null)
                    {
                        IDragAndDropComponent.Drag(hit.point);
                    }
               }
            }
        }

        // Left Click Release -- put the piece down
        if (Input.GetMouseButtonUp(0))
        {
            if (clickedObject != null)
            {
                if (IDragAndDropComponent != null)
                {
                    IDragAndDropComponent.Drop();
                }
            }

            ResetClickedObject();

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

    private void ResetClickedObject()
    {
        clickedObject = null;
        PreviousTileLoc = Vector3.zero;
        IDragAndDropComponent = null;
    }
}
