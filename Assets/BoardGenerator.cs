using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    public int sizeX = 8;
    public int sizeY = 8;

	// Use this for initialization
	void Start () {
        GenerateBoard();
    }
	

    void GenerateBoard()
    {
        GameObject tile = (GameObject)Resources.Load("BoardTilePrototype");
        float ObjectScale = tile.transform.localScale.x;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                float offsetX = (ObjectScale * sizeX) / 2;
                float offsetY = (ObjectScale * sizeY) / 2;
                Instantiate(tile, new Vector3(ObjectScale * x - offsetX, 0 , ObjectScale * y - offsetY), Quaternion.identity);
            }
        }
    }
}
