using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChecker : MonoBehaviour
{
    CutScenes cutScenes;
    void Start()
    {
        cutScenes = GetComponentInParent<CutScenes>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            cutScenes.LocationCheck();
        }
    }
}
