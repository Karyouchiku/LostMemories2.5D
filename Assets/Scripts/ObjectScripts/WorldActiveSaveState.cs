using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldActiveSaveState : MonoBehaviour, ISaveable
{
    public GameObject classRoom;
    public GameObject schoolHallway;
    public GameObject _MCHouseOutside;
    public GameObject _MCHouseInterior;
    public GameObject outsideSchool;
    public GameObject smallTown;
    public GameObject bigCity;
    public GameObject trailerPark;

    
    [Header("Render Worlds")]
    public bool renderClassRoom;
    public bool renderSchoolHallway;
    public bool renderMCHouseOutside;
    public bool renderMCHouseInterior;
    public bool renderOutsideSchool;
    public bool renderSmallTown;
    public bool renderBigCity;
    public bool renderTrailerPark;

    void Start()
    {
        StartRender();
    }
    public void RenderWorlds(bool w1, bool w2, bool w3, bool w4, bool w5, bool w6, bool w7, bool w8)
    {
        renderClassRoom = w1;
        renderSchoolHallway = w2;
        renderMCHouseOutside = w3;
        renderMCHouseInterior = w4;
        renderOutsideSchool = w5;
        renderSmallTown = w6;
        renderBigCity = w7;
        renderTrailerPark = w8;
    }
    public void StartRender()
    {
        WorldRenderer(classRoom, renderClassRoom);
        WorldRenderer(schoolHallway, renderSchoolHallway);
        WorldRenderer(_MCHouseOutside, renderMCHouseOutside);
        WorldRenderer(_MCHouseInterior, renderMCHouseInterior);
        WorldRenderer(outsideSchool, renderOutsideSchool);
        WorldRenderer(smallTown, renderSmallTown);
        WorldRenderer(bigCity, renderBigCity);
        WorldRenderer(trailerPark, renderTrailerPark);

    }

    void WorldRenderer(GameObject world, bool render)
    {
        MeshRenderer[] mesh = world.GetComponentsInChildren<MeshRenderer>();
        AudioSource[] sfx = world.GetComponentsInChildren<AudioSource>();
        Light[] lights = world.GetComponentsInChildren<Light>();

        for (int i = 0; i < mesh.Length; i++)
        {
            mesh[i].enabled = render;
        }
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].enabled = render;
        }
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = render;
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            renderClassRoom = this.renderClassRoom,
            renderSchoolHallway = this.renderSchoolHallway,
            renderMCHouseOutside = this.renderMCHouseOutside,
            renderMCHouseInterior = this.renderMCHouseInterior,
            renderOutsideSchool = this.renderOutsideSchool,
            renderSmallTown = this.renderSmallTown,
            renderBigCity = this.renderBigCity,
            renderTrailerPark = this.renderTrailerPark
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;

        renderClassRoom = saveData.renderClassRoom;
        renderSchoolHallway = saveData.renderSchoolHallway;
        renderMCHouseOutside = saveData.renderMCHouseOutside;
        renderMCHouseInterior = saveData.renderMCHouseInterior;
        renderOutsideSchool = saveData.renderOutsideSchool;
        renderSmallTown = saveData.renderSmallTown;
        renderTrailerPark = saveData.renderTrailerPark;

        StartRender();

    }

    [Serializable]
    struct SaveData
    {
        public bool renderClassRoom;
        public bool renderSchoolHallway;
        public bool renderMCHouseOutside;
        public bool renderMCHouseInterior;
        public bool renderOutsideSchool;
        public bool renderSmallTown;
        public bool renderBigCity;
        public bool renderTrailerPark;
    }
}
