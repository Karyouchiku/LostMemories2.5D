using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutScene : MonoBehaviour
{
    CutScenes cutScenes;

    void Start()
    {
        cutScenes = GetComponentInParent<CutScenes>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burito")
        {
            cutScenes.StartMoving();
        }
    }
}
