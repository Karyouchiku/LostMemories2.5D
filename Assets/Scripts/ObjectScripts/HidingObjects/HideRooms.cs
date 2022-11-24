using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRooms : MonoBehaviour
{
    MeshRenderer[] mesh;
    Light[] lights;
    SpriteRenderer[] sprites;
    Collider other;

    void Start()
    {
        mesh = GetComponentsInChildren<MeshRenderer>();
        lights = GetComponentsInChildren<Light>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if (other == null || other.tag != "Burito")
        {
            Renderer(false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burito")
        {
            Renderer(true);
            this.other = other;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Burito")
        {
            Renderer(false);
            this.other = null;
        }
    }

    void Renderer(bool render)
    {
        foreach (MeshRenderer item in mesh)
        {
            item.enabled = render;
        }
        foreach (Light light in lights)
        {
            light.enabled = render;
        }
        foreach (SpriteRenderer sprite in sprites)
        {
            try
            {
                if (sprite.GetComponentInParent<Item>().isActive)
                {
                    sprite.enabled = render;
                }
            }
            catch
            {
                sprite.enabled = render;
            }
        }
    }
}
