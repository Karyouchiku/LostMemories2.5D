using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class LocationChangerWithMSpeed : MonoBehaviour
{
    CutScenes cutScene;
    public int actorID;
    public int changeLocationID;
    public float MoveSpeed;
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
            cutScene.ChangeLocation(actorID, changeLocationID, MoveSpeed);
            gameObject.SetActive(false);
        }
    }
}
