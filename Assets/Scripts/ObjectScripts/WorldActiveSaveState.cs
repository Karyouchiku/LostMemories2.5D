using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldActiveSaveState : MonoBehaviour, ISaveable
{
    [Header("Using Directional Light")]
    public Light directionalLight;
    [Header("GameObject Worlds")]
    public GameObject[] places;
   
    [Header("Enable Lighting")]
    public bool lighting;
    [Header("Render World ID")]
    public int renderPlaceID;
    
    void Start()
    {
        RenderWorlds(lighting, renderPlaceID);
    }
    public void RenderWorlds(bool lighting, int renderPlaceID)//bool w1, bool w2, bool w3, bool w4, bool w5, bool w6, bool w7, bool w8, bool w9, bool w10, bool w11)
    {
        this.lighting = lighting;
        this.renderPlaceID = renderPlaceID;
        directionalLight.enabled = this.lighting;
        WorldRenderer(renderPlaceID);

    }
    
    void WorldRenderer(int renderPlaceID)
    {
        

        for (int i = 0; i < places.Length; i++)
        {
            bool render = i == renderPlaceID;
            
            //places[i].SetActive(render);
            
            MeshRenderer[] mesh = places[i].GetComponentsInChildren<MeshRenderer>();
            AudioSource[] sfx = places[i].GetComponentsInChildren<AudioSource>();
            Light[] lights = places[i].GetComponentsInChildren<Light>();
            SpriteRenderer[] sprites = places[i].GetComponentsInChildren<SpriteRenderer>();

            for (int j = 0; j < mesh.Length; j++)
            {
                mesh[j].enabled = render;
            }
            for (int j = 0; j < sfx.Length; j++)
            {
                sfx[j].enabled = render;
            }
            for (int j = 0; j < lights.Length; j++)
            {
                lights[j].enabled = render;
            }
            for (int j = 0; j < sprites.Length; j++)
            {
                sprites[j].enabled = render;
            }
            
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            lighting = this.lighting,
            renderPlaceID = this.renderPlaceID
           
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;

        lighting = saveData.lighting;
        renderPlaceID = saveData.renderPlaceID;

        RenderWorlds(lighting, renderPlaceID);
       
    }

    [Serializable]
    struct SaveData
    {
        public bool lighting;
        public int renderPlaceID;
       
    }
}
