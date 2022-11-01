using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMActors : MonoBehaviour, ISaveable
{
    public GameObject[] _LMActors;
    public Vector3[] orginalActorLocations;

    public void LoadState(object state)
    {
        throw new System.NotImplementedException();
    }

    public object SaveState()
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        orginalActorLocations = new Vector3[_LMActors.Length];
        for (int i = 1; i < _LMActors.Length; i++)
        {
            orginalActorLocations[i] = _LMActors[i].transform.position;
        }
    }
}
