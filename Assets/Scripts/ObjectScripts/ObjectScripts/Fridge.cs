using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burito")
        {
            PlayAnim(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Burito")
        {
            PlayAnim(false);
        }
    }
    void PlayAnim(bool open)
    {
        anim.SetBool("isOpen", open);
    }

}
