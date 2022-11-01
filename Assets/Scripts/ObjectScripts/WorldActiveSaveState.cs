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
    /*old version
    public GameObject classRoom;
    public GameObject schoolHallway;
    public GameObject _MCHouseOutside;
    public GameObject _MCHouseInterior;
    public GameObject outsideSchool;
    public GameObject smallTown;
    public GameObject bigCity;
    public GameObject trailerPark;
    public GameObject florHouse;
    public GameObject warehouse;
    public GameObject adoptionAgency;
    */

    [Header("Enable Lighting")]
    public bool lighting;
    [Header("Render World ID")]
    public int renderPlaceID;
    /* old version
    public bool renderClassRoom;
    public bool renderSchoolHallway;
    public bool renderMCHouseOutside;
    public bool renderMCHouseInterior;
    public bool renderOutsideSchool;
    public bool renderSmallTown;
    public bool renderBigCity;
    public bool renderTrailerPark;
    public bool renderFlorHouse;
    public bool renderWarehouse;
    public bool renderAdoptionAgency;
    */
    
    void Start()
    {
        //StartRender();
        RenderWorlds(lighting, renderPlaceID);
    }
    public void RenderWorlds(bool lighting, int renderPlaceID)//bool w1, bool w2, bool w3, bool w4, bool w5, bool w6, bool w7, bool w8, bool w9, bool w10, bool w11)
    {
        this.lighting = lighting;
        directionalLight.enabled = this.lighting;
        WorldRenderer(renderPlaceID);

        /*
        renderClassRoom = w1;
        renderSchoolHallway = w2;
        renderMCHouseOutside = w3;
        renderMCHouseInterior = w4;
        renderOutsideSchool = w5;
        renderSmallTown = w6;
        renderBigCity = w7;
        renderTrailerPark = w8;
        renderFlorHouse = w9;
        renderWarehouse = w10;
        renderAdoptionAgency = w11;
        */
    }
    /*
    public void StartRender()
    {
        directionalLight.enabled = lighting;
        WorldRenderer(classRoom, renderClassRoom);
        WorldRenderer(schoolHallway, renderSchoolHallway);
        WorldRenderer(_MCHouseOutside, renderMCHouseOutside);
        WorldRenderer(_MCHouseInterior, renderMCHouseInterior);
        WorldRenderer(outsideSchool, renderOutsideSchool);
        WorldRenderer(smallTown, renderSmallTown);
        WorldRenderer(bigCity, renderBigCity);
        WorldRenderer(trailerPark, renderTrailerPark);
        WorldRenderer(florHouse, renderFlorHouse);
        WorldRenderer(warehouse, renderWarehouse);
        
    }
    */
    void WorldRenderer(int renderPlaceID)
    {
        

        for (int i = 0; i < places.Length; i++)
        {
            bool render;
            if (i == renderPlaceID)
            {
                render = true;
            }
            else
            {
                render = false;
            }

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
            /*
            renderClassRoom = this.renderClassRoom,
            renderSchoolHallway = this.renderSchoolHallway,
            renderMCHouseOutside = this.renderMCHouseOutside,
            renderMCHouseInterior = this.renderMCHouseInterior,
            renderOutsideSchool = this.renderOutsideSchool,
            renderSmallTown = this.renderSmallTown,
            renderBigCity = this.renderBigCity,
            renderTrailerPark = this.renderTrailerPark,
            renderFlorHouse = this.renderFlorHouse,
            renderWarehouse = this.renderWarehouse
            */
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;

        lighting = saveData.lighting;
        renderPlaceID = saveData.renderPlaceID;

        RenderWorlds(lighting, renderPlaceID);
        /*
        renderClassRoom = saveData.renderClassRoom;
        renderSchoolHallway = saveData.renderSchoolHallway;
        renderMCHouseOutside = saveData.renderMCHouseOutside;
        renderMCHouseInterior = saveData.renderMCHouseInterior;
        renderOutsideSchool = saveData.renderOutsideSchool;
        renderSmallTown = saveData.renderSmallTown;
        renderTrailerPark = saveData.renderTrailerPark;
        renderFlorHouse = saveData.renderFlorHouse;
        renderWarehouse = saveData.renderWarehouse;

        StartRender();
        */
    }

    [Serializable]
    struct SaveData
    {
        public bool lighting;
        public int renderPlaceID;
        /*
        public bool renderClassRoom;
        public bool renderSchoolHallway;
        public bool renderMCHouseOutside;
        public bool renderMCHouseInterior;
        public bool renderOutsideSchool;
        public bool renderSmallTown;
        public bool renderBigCity;
        public bool renderTrailerPark;
        public bool renderFlorHouse;
        public bool renderWarehouse;
        */
    }
}
