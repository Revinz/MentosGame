using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Level data for converting to and from JSON file.
/// 
/// Data must be converted into a 'Level' class before use in-game or level editor. 
/// </summary>

[Serializable]
public class LevelData
{
    public int levelID;
    public string levelName;
    public int rows;
    public int columns;
    public int[] boardData; //Contains the Json data for the pieces

    public LevelData(int _rows, int _columns, string _name, int _levelID)
    {
        rows = _rows;
        columns = _columns;
        levelName = _name;
        levelID = _levelID;
        boardData = new int[_rows * _columns];
    }
}
