using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerZone : MonoBehaviour
{
    public GameObject interactButton;
    string objectName;
    void ButtonEnabler(bool turn)
    {
        interactButton.SetActive(turn);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractableNPC")
        {
            ButtonEnabler(true);
            objectName = other.gameObject.tag;
        }
        if (other.gameObject.name == "Door")
        {
            ButtonEnabler(true);
            objectName = other.gameObject.name;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InteractableNPC")
        {
            ButtonEnabler(false);
        }
        if (other.gameObject.name == "Door")
        {
            ButtonEnabler(false);
        }
    }



    public void Interactbutton()
    {
        
        if (objectName == "InteractableNPC")
        {
            Debug.Log("having a FUCKING conversation");
        }
        if (objectName == "Door")
        {
            Debug.Log("Go inside to the FUCKING Door");
        }
        
    }
}
