using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueAutoTriggerV2 : MonoBehaviour
{
    DialogueSystemTrigger trigger;

    [Header("Conversation Actor")]
    public int actorID;
    [Header("Change Conversation")]
    public bool changeConvo;
    public DialogueDatabase dialogueDatabase;
    public int convoID;
    LMActors lmActors;
    void Start()
    {
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        trigger = lmActors._LMActors[actorID].GetComponent<DialogueSystemTrigger>();
    }
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
