using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueAutoTrigger : MonoBehaviour
{
    public DialogueSystemTrigger trigger;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burito")
        {
            trigger.OnUse();
        }
    }
}
