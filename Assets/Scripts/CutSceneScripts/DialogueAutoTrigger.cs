using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueAutoTrigger : MonoBehaviour
{
    public DialogueSystemTrigger trigger;
    [Header("Change Conversation")]
    public bool changeConvo;
    public DialogueDatabase dialogueDatabase;
    public int convoID;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burito")
        {
            trigger.trigger = DialogueSystemTriggerEvent.OnUse;
            if (changeConvo)
            {
                trigger.conversation = dialogueDatabase.GetConversation(convoID).Title;
            }
            trigger.OnUse();
        }
    }
}
