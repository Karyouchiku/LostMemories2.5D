using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTriggerZone : MonoBehaviour
{
    

    [Header("Interact Button in Canvas")]
    public GameObject interactButton;

    [Header("PlayerInventory Script")]
    public PlayerInventory inventory;

    [Header("Current Interactable Object (Should be leave null)")]
    public Collider _GOCollider;
    bool isTalking;

    void OnEnable()
    {
        Doors.OnTriggerExitBtn += DisableBtn;
    }

    void OnDisable()
    {
        Doors.OnTriggerExitBtn -= DisableBtn;
    }
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
    void OnTriggerExit()
    {
        DisableBtn();
    }
    void DisableBtn()
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
    
    void DefineGameObjectTag(Collider other)
    {
        if (other == null)
        {
            return;
        }
        if (!isTalking)
        {
            switch (other.tag)
            {
                case "InteractableNPC":
                case "InteractableObject":
                    ButtonEnabler(true);
                    _GOCollider = other;
                    break;
            }
        }
    }

    public void Interactbutton()
    {
        if (_GOCollider.tag == "InteractableObject")
        {
            if (_GOCollider.gameObject.TryGetComponent<IInteractor>(out IInteractor interact))
            {
                interact.Interact();
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
