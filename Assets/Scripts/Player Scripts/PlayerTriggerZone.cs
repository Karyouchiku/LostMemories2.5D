using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTriggerZone : MonoBehaviour
{
    [Header("Interact Button in Canvas")]
    public GameObject interactButton;

    [Header("PlayerInventory Script")]
    public PlayerInventory inventory;

    [Header("Current Interactable Object (Should be leave null)")]
    public Collider other;

    void ButtonEnabler(bool turn)
    {
        interactButton.SetActive(turn);
    }
    
    void OnTriggerEnter(Collider other)
    {
        string interactButtonName = "";
        switch (other.tag)
        {
            case "InteractableNPC":
            case "InteractableObject":
                ButtonEnabler(true);
                if (other.TryGetComponent<Conversation>(out Conversation conversation))
                {
                    interactButtonName = conversation.dialogueData.npcName;
                }
                else
                {
                    interactButtonName = other.name;
                }
                interactButton.GetComponentInChildren<TextMeshProUGUI>().text = interactButtonName;
                this.other = other;
                break;
        }
    }

    void OnTriggerExit()
    {
        ButtonEnabler(false);
        other = null;
    }


    public void Interactbutton()
    {
        if (other.tag == "InteractableObject")
        {
            if (other.TryGetComponent<Doors>(out Doors door))
            {
                door.Door();
            }
        }
        else if (other.tag == "InteractableNPC")
        {
            IDialogue dialogue = other.GetComponent<IDialogue>();
            if (dialogue != null)
            {
                dialogue.Dialogue();
            }
            ButtonEnabler(false);
        }
    }
}
