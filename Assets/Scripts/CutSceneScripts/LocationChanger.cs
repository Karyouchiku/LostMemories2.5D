using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class LocationChanger : MonoBehaviour
{
    CutScenes cutScene;
    public int actorID;
    public int changeLocationID;
    LMActors lmActors;
    void Start()
    {
        cutScene = GetComponentInParent<CutScenes>();
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == lmActors._LMActors[actorID].gameObject.tag)
        {
            cutScene.ChangeLocation(actorID, changeLocationID);
            gameObject.SetActive(false);
        }
    }
}
