using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Disable Controls")]
    public GameObject joystickDisabler;
    public PlayerControls playerControls;

    public GameObject dialogueUI;
    public TextMeshProUGUI _NPCName;
    public TextMeshProUGUI dialogueBox;

    void Start()
    {
        dialogueUI.SetActive(false);
        joystickDisabler.SetActive(true);
    }
}
