using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(ItemData itemData);
    public ItemData itemData;
    public GameObject sprite;

    bool collected;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Collect()
    {
        collected = true;
        OnItemCollected?.Invoke(itemData);
        sprite.SetActive(false);
        audioSource.Play();
        
        Debug.Log($"{itemData.displayName} is Collected");
    }

    void Update()
    {
        if (collected)
        {
            if (!audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

}
