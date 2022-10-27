using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChecker : MonoBehaviour
{
    public static event HandledOnTrigger onPlayerEnterCol;
    public delegate void HandledOnTrigger();

    public string col;
    void OnTriggerEnter(Collider other)
    {
        col = other.tag;
        if (other.tag == "Burito")
        {
            onPlayerEnterCol?.Invoke();
        }
    }
}
