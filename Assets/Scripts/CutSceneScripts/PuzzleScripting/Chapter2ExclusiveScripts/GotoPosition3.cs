using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoPosition3 : MonoBehaviour
{
    IPuzzle2 puzzle;
    public int actorID;
    public int changeLocationID;
    public float moveSpeed;
    public float waitSec;
    LMActors lmActors;
    void Start()
    {
        puzzle = GetComponentInParent<IPuzzle2>();
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == lmActors._LMActors[actorID].gameObject.name)
        {
            puzzle.GotoPosition(actorID, changeLocationID, moveSpeed, waitSec);
            gameObject.SetActive(false);
        }
    }
}