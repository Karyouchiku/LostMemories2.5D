using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedOnTriggerCollider : MonoBehaviour
{
    Animator anim;
    public AudioClip open;
    public AudioClip close;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        audioSource.volume = 0.5f;
        audioSource.spatialBlend = 1;
    }
    void PlaySFX(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    void OnTriggerEnter(Collider other)
    {
        PlayAnim(other, true);
    }
    void OnTriggerExit(Collider other)
    {
        PlayAnim(other, false);
    }

    void PlayAnim(Collider other, bool isOpen)
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
