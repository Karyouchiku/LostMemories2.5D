using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingOfPuzzle2 : MonoBehaviour
{
    public PuzzleCh2 puzzle;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            puzzle.EndingPuzzle();
            Debug.Log("Puzzle 2 Endded");
        }
    }
}
