using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    IPuzzle puzzle;
    public int PostionID;
    void Start()
    {
        puzzle = GetComponentInParent<IPuzzle>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            puzzle.RestrictedArea(PostionID);
        }
    }
}
