using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class LoadData
{
    public static int saveDataID;
    public static bool isOnLoadGameData;

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

    /* Old string paths
    //MainMenu
    public static string Settings => $"{Application.persistentDataPath}/Settings.lm";

    //Checkpoints
    public static string Checkpoints => $"{Application.persistentDataPath}/LostMemoriesCheckpoint.lm";

    //Slots
    public static string SavePath1 => $"{Application.persistentDataPath}/LostMemories1.lm";
    public static string SavePath2 => $"{Application.persistentDataPath}/LostMemories2.lm";
    public static string SavePath3 => $"{Application.persistentDataPath}/LostMemories3.lm";
    public static string SavePath4 => $"{Application.persistentDataPath}/LostMemories4.lm";
    public static string SavePath5 => $"{Application.persistentDataPath}/LostMemories5.lm";
    */

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
