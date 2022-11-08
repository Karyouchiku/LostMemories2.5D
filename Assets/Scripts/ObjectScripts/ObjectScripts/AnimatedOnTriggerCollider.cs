using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedOnTriggerCollider : MonoBehaviour
{
    Animator anim;
    public AudioClip open;
    public AudioClip close;
    AudioSource audioSource;
    public bool isUnlocked = true;
    //LockInteractableDoors lockInteractableDoors;
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
        if (other.tag == "Burito" || other.tag == "InteractableNPC")
        {
            if (TryGetComponent<LockInteractableDoors>(out LockInteractableDoors door))
            {
                if (door.isUnlocked)
                {
                    gameObject.tag = "Untagged";
                    PlaySFX(open);
                }
                return;
            }
            PlaySFX(open);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (isUnlocked)
        {
            PlayAnim(other, true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (isUnlocked)
        {
            PlayAnim(other, false);
        }
    }

    void PlayAnim(Collider other, bool isOpen)
    {
        if (other.tag == "Burito" || other.tag == "InteractableNPC")
        {
            if (!isOpen)
            {
                PlaySFX(close);
            }
            anim.SetBool("isOpen", isOpen);
        }
    }
}
