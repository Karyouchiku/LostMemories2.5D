using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFloors : MonoBehaviour
{
    //Exclusive to Warehouse Interior
    MeshRenderer[] mesh;
    Light[] lights;
    public static bool enableDisablingActors;
    public GameObject[] actors;

    void Start()
    {
        mesh = GetComponentsInChildren<MeshRenderer>();
        lights = GetComponentsInChildren<Light>();
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burito")
        {
            Renderer(false);
            Debug.Log($"Wala si Burito sa: {gameObject.name}");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Burito")
        {
            Renderer(true);
            Debug.Log($"Wala si Burito sa: {gameObject.name}");
        }
    }
    void Renderer(bool render)
    {
        for (int i = 0; i < mesh.Length; i++)
        {
            mesh[i].enabled = render;
        }
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = render;
        }
        if (enableDisablingActors)
        {
            foreach (GameObject sprite in actors)
            {
                sprite.GetComponentInChildren<SpriteRenderer>().enabled = render;
            }
        }
    }
    public void ManualEnableActorSprites(bool render)
    {
        foreach (GameObject sprite in actors)
        {
            sprite.GetComponentInChildren<SpriteRenderer>().enabled = render;
        }
    }
}
