using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem.Wrappers;

public class SaveValuesInDDB : MonoBehaviour, ISaveable
{
    public DialogueDatabase dialoguedb;

    float personalityValue;

    public void SavePersonalityValue()
    {
        personalityValue = dialoguedb.variables[0].InitialFloatValue;
    }
    public void LoadPersonalityValue()
    {
        dialoguedb.variables[0].InitialFloatValue = personalityValue;
    }

    public object SaveState()
    {
        SavePersonalityValue();
        return new SaveData()
        {
            personalityValue = this.personalityValue
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.personalityValue = saveData.personalityValue;
        LoadPersonalityValue();
    }


    [Serializable]
    struct SaveData
    {
        public float personalityValue;
    }
}
