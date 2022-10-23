using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    public PlayerName playerName;

    public TextMeshProUGUI inputPlayerName;


    //for Testing
    public TextMeshProUGUI displayPlayerName;


    public void SetPlayerName()
    {
        playerName.playerName = inputPlayerName.text;

        displayPlayerName.text = playerName.playerName;
    }

}
