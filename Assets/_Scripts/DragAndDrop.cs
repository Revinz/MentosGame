using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    GameObject clickedObject;
    public LayerMask WhatIsBoard = 1 << 10;
    public LayerMask WhatIsDraggable = 1 << 11;

    public float HoverHeight = 3f;
    public float PlacedHeight = 0.65f;

    private Vector3 offset = Vector3.zero;

    private void Update()
    {
        // Left Click
        if (Input.GetMouseButtonDown(0)) //Left Click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitLoc;

            if (Physics.Raycast(ray, out hit, 100.0f, WhatIsDraggable))
            {
                Debug.Log("Object clicked: " + hit.transform.parent.gameObject.name.ToString());
                hitLoc = hit.point;
                clickedObject = hit.transform.parent.gameObject;
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

        // Left Click Drag
        if (Input.GetMouseButton(0))    
        {
            if (clickedObject != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit; 

                if (Physics.Raycast(ray, out hit, 100.0f, WhatIsBoard))
                {
                    Debug.Log("Object Dragging: " + clickedObject.name.ToString());
                    clickedObject.transform.position = new Vector3(hit.point.x + offset.x, HoverHeight, hit.point.z + offset.z);

                    //Make the child objects stay at the proper location
                    for (int i = 0; i < clickedObject.transform.childCount; i++)
                    {
                        clickedObject.transform.GetChild(i).transform.localPosition = new Vector3(0,0,0);
                    }
                }
            }
        }

        // Left Click Release
        if (Input.GetMouseButtonUp(0))
        {
            if (clickedObject != null)
            {

                Ray ray = new Ray(clickedObject.transform.position, -clickedObject.transform.up);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 50.0f, WhatIsBoard)) {
                    Debug.Log("Hit Board");
                    clickedObject.transform.position = new Vector3(hit.transform.position.x, PlacedHeight, hit.transform.position.z);
                }

            }
            clickedObject = null;
        }
    }

}
