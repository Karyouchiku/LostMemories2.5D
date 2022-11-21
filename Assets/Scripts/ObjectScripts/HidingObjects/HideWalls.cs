using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWalls : MonoBehaviour
{
    
    MeshRenderer[] walls;
    void Start()
    {
        walls = GetComponentsInChildren<MeshRenderer>();
    }

    void OnTriggerStay(Collider other)
    {
        HidingWalls(other, false);
    }
    void OnTriggerExit(Collider other)
    {
        HidingWalls(other, true);
    }

    void HidingWalls(Collider other, bool hide)
    {
        if (other.tag == "Burito")
        {
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = hide;
            }
        }
    }
}
