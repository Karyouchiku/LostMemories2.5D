using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using TMPro;
public class SaveValuesInDDB : MonoBehaviour, ISaveable
{
    [Header("For Debugging")]
    public TMP_Text PPointsText;


    double personalityValue;

    
    public void SetPersonalityPointsValue(double value)
    {
        personalityValue += value;
    }
    public double GetPersonalityPointsValue()
    {
        return personalityValue;
    }
    //FOR DEBUGGING
    
    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        Lua.RegisterFunction("GetPersonalityPoints", this, SymbolExtensions.GetMethodInfo(() => GetPersonalityPointsValue()));
        Lua.RegisterFunction("AddPersonalityPoints", this, SymbolExtensions.GetMethodInfo(() => SetPersonalityPointsValue((double)0)));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("GetPersonalityPoints");
        Lua.UnregisterFunction("AddPersonalityPoints");
    }
    void Update()
    {
        PPointsText.text = $"Personality Points: {personalityValue}";
    }

    //----For debugging

    public object SaveState()
    {
        return new SaveData()
        {
            personalityValue = this.personalityValue
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.personalityValue = saveData.personalityValue;
    }


    [Serializable]
    struct SaveData
    {
        public double personalityValue;
    }
}
