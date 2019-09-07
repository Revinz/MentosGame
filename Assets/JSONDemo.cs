using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONDemo : MonoBehaviour
{
    string path;
    string jsonString;

    private void Start()
    {
        //Loading the data
        path = Application.streamingAssetsPath + "/JSONDATA_LEARNING/Creature.json";
        jsonString = File.ReadAllText(path);
        Creature Yumo = JsonUtility.FromJson<Creature>(jsonString);
        
        //Editing the data
        Yumo.Level = 21;


        //Saving the data
        string newYumo = JsonUtility.ToJson(Yumo);
        Debug.Log(newYumo);
        File.WriteAllText(path, newYumo);
    }
}


public class Creature
{

    public string Name;
    public int Level;
    public int[] Stats;

}