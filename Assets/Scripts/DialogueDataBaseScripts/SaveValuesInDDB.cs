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
    public DialogueDatabase dialogueDatabase;

    #region Variables
    double personalityValue;

    #endregion
    #region Methods
    //For Condition
    public double GetPersonalityPointsValue()
    {
        return personalityValue;
    }

    //Modify
    public void SetPersonalityPointsValue(double value)
    {
        personalityValue += value;
    }
    #endregion
    #region Lua function registrations
    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        //For Conditions
        Lua.RegisterFunction("GetPersonalityPoints", this, SymbolExtensions.GetMethodInfo(() => GetPersonalityPointsValue()));
        


        //For Modifying
        Lua.RegisterFunction("AddPersonalityPoints", this, SymbolExtensions.GetMethodInfo(() => SetPersonalityPointsValue((double)0)));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("GetPersonalityPoints");
        Lua.UnregisterFunction("AddPersonalityPoints");
    }
    #endregion
    #region Save System
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
    #endregion
}
