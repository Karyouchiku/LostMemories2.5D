using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystemInMainMenu : MonoBehaviour
{
    //For settings
    public void SaveMainMenuSettings()
    {
        var state = LoadFile(LoadData.SaveDatas[6]);
        SaveState(state);
        FileStreamingManager.SaveFile(state, LoadData.SaveDatas[6]);
    }
    public void LoadMainMenuSettings()
    {
        var state = LoadFile(LoadData.SaveDatas[6]);
        LoadState(state);
    }

    Dictionary<string, object> LoadFile(string SavePath)
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No Savegame Data\nCreated a new save game File");
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.SaveState();
        }
    }
    void LoadState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object savedState))
            {
                saveable.LoadState(savedState);
            }

        }
    }
}
