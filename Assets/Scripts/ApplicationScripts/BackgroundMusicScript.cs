using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    AudioSource bgMusic;
    [Range(0,1)]
    public float maxVolume = 0.6f;

    [Range(0.1f,5f)]
    public float fadeSpeed = 1.5f;
    bool turn;
    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
        bgMusic.playOnAwake = true;
        bgMusic.loop = true;
        bgMusic.spatialBlend = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (turn)
        {
            if (bgMusic.volume < maxVolume)
            {
                bgMusic.volume += fadeSpeed * Time.deltaTime;
            }
            if (bgMusic.volume > maxVolume)
            {
                bgMusic.volume -= fadeSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (bgMusic.volume > 0)
            {
                bgMusic.volume -= fadeSpeed * Time.deltaTime;
            }
        }
    }
    void OnEnable()
    {
        turn = true;
    }
    void OnDisable()
    {
        bgMusic.volume = 0;
        turn = false;
    }
}
