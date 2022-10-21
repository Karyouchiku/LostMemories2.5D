using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDoors : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        DoorAnimation(other, true);
    }
    void OnTriggerExit(Collider other)
    {
        DoorAnimation(other, false);
    }

    void DoorAnimation(Collider other, bool isOpen)
    {
        if (other.tag == "Burito")
        {
            anim.SetBool("isOpen", isOpen);
        }
    }
}
