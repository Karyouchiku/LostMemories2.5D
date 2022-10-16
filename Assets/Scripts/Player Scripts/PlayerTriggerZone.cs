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
    public Collider _GOCollider;
    string interactButtonName;
    bool isTalking;

    void ButtonEnabler(bool turn)
    {
        interactButton.SetActive(turn);
    }
    
    void OnTriggerEnter(Collider other)
    {
        DefineGameObjectTag(other);
    }
    void OnTriggerStay(Collider other)
    {
        DefineGameObjectTag(other);
    }

    void DefineGameObjectTag(Collider other)
    {
        if (!isTalking)
        {
            switch (other.tag)
            {
                case "InteractableNPC":
                case "InteractableObject":
                    interactButtonName = other.name;
                    ButtonEnabler(true);

                    interactButton.GetComponentInChildren<TextMeshProUGUI>().text = interactButtonName;
                    _GOCollider = other;
                    break;
            }
        }
    }
    void OnTriggerExit()
    {
        _GOCollider = null;
        isTalking = false;
    }
    void FixedUpdate()
    {
        if (_GOCollider == null)
        {
            ButtonEnabler(false);
        }
    }
    public void Interactbutton()
    {
        if (_GOCollider.tag == "InteractableObject")
        {
            if (_GOCollider.TryGetComponent<Doors>(out Doors door))
            {
                door.Door();
            }
            if (_GOCollider.TryGetComponent<Gate>(out Gate gate))
            {
                gate.OpenGate();
            }
        }
        else if (_GOCollider.tag == "InteractableNPC")
        {
            Debug.Log("Convo Test");
            ButtonEnabler(false);
            isTalking = true;
        }
    }
}
