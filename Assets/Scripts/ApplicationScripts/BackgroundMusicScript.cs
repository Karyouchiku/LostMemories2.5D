using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    AudioSource bgMusic;
    [Range(0,1)]
    public float maxVolume = 0.6f;
    void Start()
    {
        bgMusic = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (bgMusic.volume < maxVolume)
        {
            bgMusic.volume += 1.5f * Time.deltaTime;
        }
        if (bgMusic.volume > maxVolume)
        {
            bgMusic.volume -= 1.5f * Time.deltaTime;
        }
    }
    void OnDisable()
    {
        bgMusic.volume = 0;
    }
}
