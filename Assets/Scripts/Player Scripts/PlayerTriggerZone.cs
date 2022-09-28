using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerZone : MonoBehaviour
{
    public GameObject interactButton;
    //string objectName;

    public PlayerInventory inventory;
    Collider other;

    void ButtonEnabler(bool turn)
    {
        interactButton.SetActive(turn);
    }
    
    void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "InteractableNPC":
            case "InteractableObject":
                ButtonEnabler(true);
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
            other.GetComponent<Doors>().Door();
        }
    }
}
