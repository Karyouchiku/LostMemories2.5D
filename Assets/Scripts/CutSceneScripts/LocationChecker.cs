using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChecker : MonoBehaviour
{
    CutScenes cutScenes;
    LMActors lmActors;
    void Start()
    {
        cutScenes = GetComponentInParent<CutScenes>();
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito" || other.tag == "InteractableNPC" || other.tag == "NPC")
        {
            cutScenes.LocationCheck();
            Debug.Log(other.name);
        }
    }
}
