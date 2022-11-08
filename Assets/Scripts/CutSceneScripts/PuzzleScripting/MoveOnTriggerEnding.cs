using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTriggerEnding : MonoBehaviour
{
    IPuzzle puzzle;
    public float moveSpeed;
    public int MoveToPositionID;
    void Start()
    {
        puzzle = GetComponentInParent<IPuzzle>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            puzzle.FinishingPuzzle();
            puzzle.MovePlayer(true, moveSpeed, MoveToPositionID);
        }
    }

}
