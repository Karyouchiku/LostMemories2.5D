using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnterDoor : MonoBehaviour
{
    public CutScenes cutScene;

    void Start()
    {
        cutScene = GetComponentInParent<CutScenes>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            cutScene.EnterDoor();
        }
    }
}
