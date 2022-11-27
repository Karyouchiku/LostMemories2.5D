using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using TMPro;
public class SaveValuesInDDB : MonoBehaviour, ISaveable
{
    #region Instance
    [Header("For Debugging")]
    public TMP_Text PPointsText;

    public DialogueDatabase dialogueDatabase;
    #endregion
    #region Variables
    double personalityValue;
    bool palomaNumber;
    bool delivered;
    double palomaPoints;

    #endregion
    #region Methods
    //For Condition
    public double GetPersonalityPointsValue()
    {
        return personalityValue;
    }

    public bool checkPalomaNumber()
    {
        return palomaNumber;
    }

    public bool getDeliveryStatus()
    {
        return delivered;
    }

    public double getPalomaPoints()
    {
        return palomaPoints;
    }

    //Modify
    public void SetPersonalityPointsValue(double value)
    {
        personalityValue += value;
    }

    public void setPalomaNumber(bool value)
    {
        palomaNumber = value;
    }

    public void setDeliveryStatus(bool value)
    {
        delivered = value;
    }

    public void setPalomaPoints(double value)
    {
        palomaPoints += value;
    }

    #endregion
    #region Lua function registrations
    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        //For Conditions
        Lua.RegisterFunction("GetPersonalityPoints", this, SymbolExtensions.GetMethodInfo(() => GetPersonalityPointsValue()));
        Lua.RegisterFunction("checkPalomaNum", this, SymbolExtensions.GetMethodInfo(() => checkPalomaNumber()));
        Lua.RegisterFunction("DeliveryStatus", this, SymbolExtensions.GetMethodInfo(() => getDeliveryStatus()));
        Lua.RegisterFunction("GetPalomaPoints", this, SymbolExtensions.GetMethodInfo(() => getPalomaPoints()));

        //For Modifying
        Lua.RegisterFunction("AddPersonalityPoints", this, SymbolExtensions.GetMethodInfo(() => SetPersonalityPointsValue((double)0)));
        Lua.RegisterFunction("PalomaNumber", this, SymbolExtensions.GetMethodInfo(() => setPalomaNumber(false)));
        Lua.RegisterFunction("Delivery", this, SymbolExtensions.GetMethodInfo(() => setDeliveryStatus(false)));
        Lua.RegisterFunction("AddPalomaPoints", this, SymbolExtensions.GetMethodInfo(() => setPalomaPoints((double)0)));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("GetPersonalityPoints");
        Lua.UnregisterFunction("AddPersonalityPoints");
        Lua.UnregisterFunction("checkPalomaNum");
        Lua.UnregisterFunction("PalomaNumber");
        Lua.UnregisterFunction("DeliveryStatus");
        Lua.UnregisterFunction("Delivery");
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
