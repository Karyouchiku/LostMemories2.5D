using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDoors : MonoBehaviour
{
    Animator anim;
    public AudioClip open;
    public AudioClip close;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    void PlaySFX(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
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
            if (isOpen)
            {
                PlaySFX(open);
            }
            else
            {
                PlaySFX(close);
            }
            anim.SetBool("isOpen", isOpen);
            
            
        }
    }

}
