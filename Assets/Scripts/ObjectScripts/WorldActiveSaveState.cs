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
    
    [Header("Render Worlds")]
    public bool renderClassRoom;
    public bool renderSchoolHallway;
    public bool renderMCHouseOutside;
    public bool renderMCHouseInterior;
    public bool renderOutsideSchool;
    public bool renderSmallTown;

    void Start()
    {
        StartRender();
    }
    public void RenderWorlds(bool _1, bool _2, bool _3, bool _4, bool _5, bool _6)
    {
        renderClassRoom = _1;
        renderSchoolHallway = _2;
        renderMCHouseOutside = _3;
        renderMCHouseInterior = _4;
        renderOutsideSchool = _5;
        renderSmallTown = _6;
    }
    public void StartRender()
    {
        WorldRenderer(classRoom, renderClassRoom);
        WorldRenderer(schoolHallway, renderSchoolHallway);
        WorldRenderer(_MCHouseOutside, renderMCHouseOutside);
        WorldRenderer(_MCHouseInterior, renderMCHouseInterior);
        WorldRenderer(outsideSchool, renderOutsideSchool);
        WorldRenderer(smallTown, renderSmallTown);

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
            renderSmallTown = this.renderSmallTown
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
    }
}
