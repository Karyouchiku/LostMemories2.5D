using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStepsSFX : MonoBehaviour
{
    public AudioClip[] woodClips;
    public AudioClip[] stoneClips;
    public AudioClip[] dirtClips;
    public AudioSource source;
    AudioClip clip;
    CharacterController controller;
    CharacterAnimation anim;

    void Start()
    {
        anim = GetComponent<CharacterAnimation>();
        source.loop = false;
    }
    void Update()
    {
        if (anim.moveX >= 0.3f || anim.moveX <= -0.3f || 
            anim.moveZ >= 0.3f || anim.moveZ <= -0.3f)
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }
    int iClip;
    void OnTriggerStay(Collider other)
    {
        //Wood
        if (other.gameObject.tag == "wood")
        {
            source.pitch = 1.4f;
            try
            {
                if (!source.isPlaying)
                {
                    iClip++;
                    clip = woodClips[iClip];
                }
            }
            catch
            {
                if (!source.isPlaying)
                {
                    iClip = 0;
                    clip = woodClips[iClip];
                }
            }
        }
        //Stone
        if (other.gameObject.tag == "stone")
        {
            source.pitch = 1.4f;
            try
            {
                if (!source.isPlaying)
                {
                    iClip++;
                    clip = stoneClips[iClip];
                }
            }
            catch
            {
                if (!source.isPlaying)
                {
                    iClip = 0;
                    clip = stoneClips[iClip];
                }
            }
        }
        //Dirt
        if (other.gameObject.tag == "dirt")
        {
            source.pitch = 1.4f;
            try
            {
                if (!source.isPlaying)
                {
                    iClip++;
                    clip = dirtClips[iClip];
                }
            }
            catch
            {
                if (!source.isPlaying)
                {
                    iClip = 0;
                    clip = dirtClips[iClip];
                }
            }
        }

        source.clip = clip;
    }
}
