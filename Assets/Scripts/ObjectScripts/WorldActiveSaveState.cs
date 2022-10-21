using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldActiveSaveState : MonoBehaviour, ISaveable
{
    public GameObject classroom;
    public GameObject outdoor;
    public GameObject hallway;
    public GameObject _MCHouseInterior;

    public object SaveState()
    {
        return new SaveData()
        {
            classroom = this.classroom.activeSelf,
            outdoor = this.outdoor.activeSelf,
            hallway = this.hallway.activeSelf,
            _MCHouseInterior = this._MCHouseInterior.activeSelf
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;

        classroom.SetActive(saveData.classroom);
        outdoor.SetActive(saveData.outdoor);
        hallway.SetActive(saveData.hallway);
        _MCHouseInterior.SetActive(saveData._MCHouseInterior);
        
    }

    [Serializable]
    struct SaveData
    {
        public bool classroom;
        public bool outdoor;
        public bool hallway;
        public bool _MCHouseInterior;
    }
}
