using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class LocationChanger : MonoBehaviour
{
    CutScenes cutScene;
    public int changeLocationID;
    public string GameObjectTag = "Burito";
    void Start()
    {
        cutScene = GetComponentInParent<CutScenes>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameObjectTag)
        {
            cutScene.ChangeLocation(changeLocationID);
        }
    }
}
