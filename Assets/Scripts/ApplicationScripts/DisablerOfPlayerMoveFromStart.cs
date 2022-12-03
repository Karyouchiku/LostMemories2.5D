using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablerOfPlayerMoveFromStart : MonoBehaviour
{
    public BlackTransitioning transition;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Burito")
        {
            transition.ManualTransitionOFF();
        }
    }
}
