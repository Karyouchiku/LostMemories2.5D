using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMActors : MonoBehaviour
{
    public GameObject[] _LMActors;
    public Vector3[] orginalActorLocations;

    void Awake()
    {
        orginalActorLocations = new Vector3[_LMActors.Length];
        for (int i = 1; i < _LMActors.Length; i++)
        {
            orginalActorLocations[i] = _LMActors[i].transform.position;
        }
    }
}
