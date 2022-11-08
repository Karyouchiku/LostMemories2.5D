using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerToMoveAutomatically : MonoBehaviour
{
    IPuzzle puzzle;

    void Start()
    {
        puzzle = GetComponentInParent<IPuzzle>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            puzzle.MovePlayer(false);
        }
    }
}
