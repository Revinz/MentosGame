using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class DataController : MonoBehaviour
{
    private static string path = Application.streamingAssetsPath + "/Levels.json";
    
    public static GameData gamedata = new GameData();

    //Load all the player data on StartUp
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        FetchBoardList()[0].spawner.Spawn();
        //TestJsonLevelEditing();
    }

    public static List<Board> FetchBoardList()
    {
        LoadAllLevels();
        List<Board> boards = new List<Board>();
        foreach (LevelData level in gamedata.Levels)
        {
            boards.Add(LevelDataConverter.ConvertToBoardFromJsonData(level));
        }
        return boards;
    }

    // Loads levels from json data into memory as LevelData
    private  static void LoadAllLevels()
    {
        gamedata.Levels.Clear();
        string jsonString = File.ReadAllText(path);

        GameData list = JsonUtility.FromJson<GameData>(jsonString);
        foreach (LevelData data in list.Levels)
        {
            gamedata.Levels.Add(data);
        }
        //Debug.Log(jsonString);
    }

    public static int FindBoardIndex(Board board)
    {



        throw new NotImplementedException();
    }    

    public static void OrderLevelList()
    {
        gamedata.Levels = gamedata.Levels.OrderBy(level => level.levelID).ToList();
    }

    public static void SaveGameData()
    {
        string newData = JsonUtility.ToJson(gamedata, true);
        //Debug.Log(newData);
        File.WriteAllText(path, newData);
        //Debug.Log("GameData Updated");
    }

}
