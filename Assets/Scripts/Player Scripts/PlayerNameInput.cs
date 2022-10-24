using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;
//using PixelCrushers.DialogueSystem.Wrappers;

public class PlayerNameInput : MonoBehaviour
{
    public DialogueDatabase dialogueDB;
    public PlayerName playerName;

    public TextMeshProUGUI inputPlayerName;


    //for Testing
    public TextMeshProUGUI displayPlayerName;
    

    public void SetPlayerName()
    {
        playerName.playerName = inputPlayerName.text;

        displayPlayerName.text = playerName.playerName;
        
        Debug.Log(dialogueDB.GetVariable("Alert").InitialValue);
        Debug.Log($"This is the Boolean: {dialogueDB.GetVariable("testBool").InitialValue}");
        dialogueDB.actors[0].Name = playerName.playerName;


    }

}
