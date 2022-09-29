using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour, IDialogue
{
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;

    public void Dialogue()
    {
        dialogueManager.dialogueUI.SetActive(true);
        dialogueManager.joystickDisabler.SetActive(false);

        dialogueManager._NPCName.text = dialogueData.npcName;
        dialogueManager.playerControls.isControlEnable = false;
        dialogueManager.dialogueBox.text = dialogueData.stringDialogueParts[0];
    }
}
