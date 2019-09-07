using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickClickMovement : MonoBehaviour {

    GameObject clickedObject;
    public LayerMask WhatIsDraggable; //Same for clickable
    public LayerMask WhatIsBoard;

    public float HoverHeight = 3f;
    public float PlacedHeight = 0.65f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Left Click
        if (Input.GetMouseButtonDown(0)) //Left Click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("Object clicked: " + hit.transform.parent.gameObject.name.ToString());
                Debug.Log("Object Layer: " + hit.transform.parent.gameObject.layer);


                if (hit.transform.parent.gameObject.layer == 10 && clickedObject != null)
                {
                    Debug.Log("Placed piece");
                    //Place down the piece on the new location
                    clickedObject.transform.position = new Vector3(hit.transform.gameObject.transform.position.x,
                        PlacedHeight, hit.transform.gameObject.transform.position.z);

                    clickedObject = null;

                } 

                else if (hit.transform.parent.gameObject.layer == 11)
                {
                    Debug.Log("Picked up piece");

                    if (clickedObject != null) //Change piece to move
                    {
                        clickedObject.transform.position = new Vector3(clickedObject.transform.position.x, PlacedHeight, clickedObject.transform.position.z);
                    }

                    //"Pick up" the piece
                    clickedObject = hit.transform.parent.gameObject;
                    clickedObject.transform.position = new Vector3(clickedObject.transform.position.x, HoverHeight, clickedObject.transform.position.z);

                }
            }

        }
    }
}
