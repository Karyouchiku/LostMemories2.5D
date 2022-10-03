using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem.Wrappers;

public class PlayerNameInDDB : MonoBehaviour
{
    public PlayerName playerName;
    public DialogueDatabase dialoguedb;

    void Awake()
    {
        dialoguedb.actors[0].Name = playerName.playerName;
    }
    
}
