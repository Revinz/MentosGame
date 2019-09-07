using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelDataEditor
{
    public static void AddLevel(int _rows, int _columns, int _levelID, string _name)
    {
        //We want new levels to start with a -1 level id since it is incomplete.        
        List<Board> boards = DataController.FetchBoardList();

        LevelData newLevel = new LevelData(_rows, _columns, _name, _levelID);


        int index = boards.FindIndex(board => board.levelID == _levelID);

        //Level already exists, move all levelData levelIDs from that levelID and up by 1,
        //since we insert before the existing level
        if (index > -1)
        {
            for (int i = index; i < boards.Count; i++)
            {
                DataController.gamedata.Levels[i].levelID++;
            }
        }

        DataController.gamedata.Levels.Add(newLevel);
        //Lastly, sort the list by levelID
        DataController.OrderLevelList();

        DataController.SaveGameData();

    }

    public static void DeleteLevel(int levelID)
    {
        GameData data = DataController.gamedata;

        for (int i = 0; i < data.Levels.Count; i++)
        {
            if (data.Levels[i].levelID == levelID)
            {
                data.Levels[i] = null;
                data.Levels.RemoveAll(item => item == null);
                DataController.SaveGameData();
                break;
            }

        }

        DataController.gamedata = data;
    }

    public static void MoveLevelUp(int _levelID)
    {
        int index = DataController.gamedata.Levels.FindIndex(level => level.levelID == _levelID);

        if (index == DataController.gamedata.Levels.Count - 1 || index == -1 || DataController.gamedata.Levels.Count == 0)
            return;

        //We want to swap the level if the next level in the list is the one right after,
        //Else we simply just need to increment the levelID for the level we want to move up.
        if (Mathf.Abs(DataController.gamedata.Levels[index + 1].levelID - _levelID) == 1)
        {
            SwapLevels(index, index + 1);
        }
        else
            DataController.gamedata.Levels[index].levelID++;

        DataController.SaveGameData();
    }

    public static void MoveLevelDown(int _levelID)
    {
        int index = DataController.gamedata.Levels.FindIndex(level => level.levelID == _levelID);

        if (index == 0 || index == -1 || DataController.gamedata.Levels.Count == 0)
            return;

        //We want to swap the level if the next level in the list is the one right after,
        //Else we simply just need to decrement the levelID for the level we want to move up.
        if (Mathf.Abs(DataController.gamedata.Levels[index - 1].levelID - _levelID) == 1)
        {
            SwapLevels(index, index - 1);
        }
            
        else
            DataController.gamedata.Levels[index].levelID--;

        DataController.SaveGameData();  
    }

    private static void SwapLevels(int levelIndex, int indexToSwapWith)
    {
        GameData data = DataController.gamedata;
        int levelToSwap = data.Levels[levelIndex].levelID;
        int levelToSwapWith = data.Levels[indexToSwapWith].levelID;
        int temp = levelToSwap;

        data.Levels[levelIndex].levelID = levelToSwapWith;
        data.Levels[indexToSwapWith].levelID = temp;

        DataController.OrderLevelList();
        DataController.SaveGameData();
    }

}
