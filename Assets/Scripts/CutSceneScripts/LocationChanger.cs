using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChanger : MonoBehaviour
{
    CutScenes cutScene;
    public int changeLocationID;
    void Start()
    {
        cutScene = GetComponentInParent<CutScenes>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            cutScene.ChangeLocation(changeLocationID);
        }
    }
}
