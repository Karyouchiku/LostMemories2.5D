using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    //Checkpoints
    public void SaveCheckpoint()
    {
        var state = LoadFile(LoadData.Checkpoints);
        SaveState(state);
        SaveFile(state, LoadData.Checkpoints);
    }
    public void LoadCheckpoint()
    {
        var state = LoadFile(LoadData.Checkpoints);
        LoadState(state);
    }


    //Save Methods
    public void Save1()
    {
        var state = LoadFile(LoadData.SavePath1);
        SaveState(state);
        SaveFile(state, LoadData.SavePath1);
    }
    public void Save2()
    {
        var state = LoadFile(LoadData.SavePath2);
        SaveState(state);
        SaveFile(state, LoadData.SavePath2);
    }
    public void Save3()
    {
        var state = LoadFile(LoadData.SavePath3);
        SaveState(state);
        SaveFile(state, LoadData.SavePath3);
    }
    public void Save4()
    {
        var state = LoadFile(LoadData.SavePath4);
        SaveState(state);
        SaveFile(state, LoadData.SavePath4);
    }
    public void Save5()
    {
        var state = LoadFile(LoadData.SavePath5);
        SaveState(state);
        SaveFile(state, LoadData.SavePath5);
    }
    
    //Load Methods
    public void Load1()
    {
        var state = LoadFile(LoadData.SavePath1);
        LoadState(state);
    }
    public void Load2()
    {
        var state = LoadFile(LoadData.SavePath2);
        LoadState(state);
    }
    public void Load3()
    {
        var state = LoadFile(LoadData.SavePath3);
        LoadState(state);
    }
    public void Load4()
    {
        var state = LoadFile(LoadData.SavePath4);
        LoadState(state);
    }
    public void Load5()
    {
        var state = LoadFile(LoadData.SavePath5);
        LoadState(state);
    }


    public void SaveFile(object state, string SavePath)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
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
