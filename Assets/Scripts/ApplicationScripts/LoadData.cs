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
        switch (id)
        {
            case 1:
                SavePath = SavePath1;
                break;
            case 2:
                SavePath = SavePath2;
                break;
            case 3:
                SavePath = SavePath3;
                break;
            case 4:
                SavePath = SavePath4;
                break;
            case 5:
                SavePath = SavePath5;
                break;
            case 6:
                SavePath = Checkpoints;
                break;
            default:
                SavePath = Checkpoints;
                break;
        }

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


    //Checkpoints
    public static string Checkpoints => $"{Application.persistentDataPath}/LostMemoriesCheckpoint.lm";

    //Slots
    public static string SavePath1 => $"{Application.persistentDataPath}/LostMemories1.lm";
    public static string SavePath2 => $"{Application.persistentDataPath}/LostMemories2.lm";
    public static string SavePath3 => $"{Application.persistentDataPath}/LostMemories3.lm";
    public static string SavePath4 => $"{Application.persistentDataPath}/LostMemories4.lm";
    public static string SavePath5 => $"{Application.persistentDataPath}/LostMemories5.lm";


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
