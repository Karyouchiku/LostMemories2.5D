using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    
    //Save Methods
    public void Save(int id)
    {
        var state = LoadFile(LoadData.SaveDatas[id]);
        SaveState(state);
        FileStreamingManager.SaveFile(state, LoadData.SaveDatas[id]);
    }
    //Load Methods
    public void Load(int id)
    {
        var state = LoadFile(LoadData.SaveDatas[id]);
        LoadState(state);
    }
    public IngameMenuScript ingameMenuScript;
    public void LoadGame(int id)
    {
        if (LoadData.SaveGameFileChecker(LoadData.SaveDatas[id]))
        {
            LoadDataCheckerInMainMenu.isThisLoadGame = true;
            LoadDataCheckerInMainMenu.saveGameID = id;
            ingameMenuScript.BackToMainMenu();
        }
        else
        {
            Debug.Log("No Save FIle Here");
        }
        

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
