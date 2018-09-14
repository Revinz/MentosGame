using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    public int size = 5;

	// Use this for initialization
	void Start () {
        GenerateBoard();
    }
	

    void GenerateBoard()
    {
        GameObject tile = (GameObject)Resources.Load("BoardTilePrototype");

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Instantiate(tile, new Vector3(1 * x, 1 * y, 0), Quaternion.identity);
            }
        }
    }
}
