using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorDataLoader
{
    public static Board FetchLevelBoard(int level)
    {
        return LevelDataConverter.ConvertToBoardFromJsonData(DataController.gamedata.Levels[level]);
    }

    public static void SaveLevel(Board board)
    {
        LevelData updatedLevelData = LevelDataConverter.ConvertToJsonLevelDataFromBoard(board);
        GameData gamedata = DataController.gamedata;
        //Debug.Log(gamedata.Levels.Count);
        for (int i = 0; i < gamedata.Levels.Count; i++)
        {
            //Debug.Log(gamedata.Levels[i].level);
            if (gamedata.Levels[i].levelID == updatedLevelData.levelID)
            {
                gamedata.Levels[i] = updatedLevelData;
                //Debug.Log("Level overwritten");
                break;
            }
        }

        DataController.SaveGameData();
    }

    public static string[] LoadDropDownLevelList()
    {

        List<string> levelList = new List<string>();
        List<Board> boardsList = DataController.FetchBoardList();

        for (int i = 0; i < boardsList.Count; i++)
        {

            levelList.Add(boardsList[i].levelID.ToString());

        }

        string[] finalList = new string[levelList.Count];
        for (int i = 0; i < finalList.Length; i++)
        {
            finalList[i] = levelList[i];
            Debug.Log(finalList[i]);
        }

        return finalList;
    }
}
