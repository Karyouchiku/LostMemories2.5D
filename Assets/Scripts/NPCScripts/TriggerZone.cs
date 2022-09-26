using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject interactButton;

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.name == "Burito")
        {
            Debug.Log(player.gameObject.name);
            interactButton.SetActive(true);
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.name == "Burito")
        {
            interactButton.SetActive(false);
        }
    }
}
