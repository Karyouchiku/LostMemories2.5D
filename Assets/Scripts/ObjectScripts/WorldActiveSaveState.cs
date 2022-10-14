using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldActiveSaveState : MonoBehaviour, ISaveable
{
    public GameObject classroom;
    public GameObject outdoor;


    public object SaveState()
    {
        return new SaveData()
        {
            classroom = this.classroom.activeSelf,
            outdoor = this.outdoor.activeSelf
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;

        classroom.SetActive(saveData.classroom);
        outdoor.SetActive(saveData.outdoor);
        
    }

    [Serializable]
    struct SaveData
    {
        public bool classroom;
        public bool outdoor;
    }
}
