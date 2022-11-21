using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFloors : MonoBehaviour
{
    //Exclusive to Warehouse Interior
    MeshRenderer[] mesh;
    Light[] lights;
    Collider other;

    void Start()
    {
        mesh = GetComponentsInChildren<MeshRenderer>();
        lights = GetComponentsInChildren<Light>();
    }
    void Update()
    {
        if (other == null)
        {
            //Renderer(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Burito")
        {
            Renderer(false);
        }
        this.other = other;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Burito")
        {
            Renderer(true);
        }
        this.other = null;
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
    }
}
