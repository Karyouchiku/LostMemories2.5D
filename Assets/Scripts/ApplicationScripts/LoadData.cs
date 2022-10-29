using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class LoadData
{
    public static int saveDataID;
    public static bool isOnLoadGameData;
    public static string[] SaveDatas =
    { 
        $"{Application.persistentDataPath}/LostMemories1.lm",           //0
        $"{Application.persistentDataPath}/LostMemories2.lm",           //1
        $"{Application.persistentDataPath}/LostMemories3.lm",           //2
        $"{Application.persistentDataPath}/LostMemories4.lm",           //3
        $"{Application.persistentDataPath}/LostMemories5.lm",           //4
        $"{Application.persistentDataPath}/LostMemoriesCheckpoint.lm",  //5
        $"{Application.persistentDataPath}/Settings.lm",                //6
        $"{Application.persistentDataPath}/LostMemories0.lm"            //7
    };

    public static bool SaveGameDataID(int id, bool isloadgame)
    {
        string SavePath;
        SavePath = SaveDatas[id];

        if (SaveGameFileChecker(SavePath))
        {
            saveDataID = id;
            isOnLoadGameData = isloadgame;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool SaveGameFileChecker(string SavePath)
    {
        if (File.Exists(SavePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
