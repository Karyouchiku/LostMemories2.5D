using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerZone : MonoBehaviour
{
    public GameObject interactButton;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractableNPC")
        {
            interactButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InteractableNPC")
        {
            interactButton.SetActive(false);
        }
    }
}
