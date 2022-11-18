using System;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMActors : MonoBehaviour, ISaveable
{
    public GameObject[] _LMActors;
    public Vector3[] orginalActorLocations;
    public DialogueDatabase dialogueDatabase;


    void Awake()
    {
        orginalActorLocations = new Vector3[_LMActors.Length];
        orginalActorLocations = GetActorsPosition();
    }

    Vector3[] GetActorsPosition()
    {
        Vector3[] actorsPosition = new Vector3[_LMActors.Length];

        for (int i = 0; i < _LMActors.Length; i++)
        {
            actorsPosition[i] = _LMActors[i].transform.position;
        }
        return actorsPosition;
    }
    string GetActorsTag(int actorID)
    {
        return _LMActors[actorID].tag;
    }

    int GetDialogueConversationID(int actorID)
    {
        int convoID;
        try
        {
            convoID = dialogueDatabase.GetConversation(_LMActors[actorID].GetComponent<DialogueSystemTrigger>().conversation).id;
        }
        catch 
        {
            //Debug.Log($"The ActorID: {actorID} has no conversation data");
            convoID = 0;
        }
        return convoID;
    }
    void SetDialogueConversation(int actorID, int convoID)
    {
        try
        {
            _LMActors[actorID].GetComponent<DialogueSystemTrigger>().conversation = dialogueDatabase.GetConversation(convoID).Title;
        }
        catch
        {
            //Debug.Log($"The ActorID: {actorID} has no conversation data");
        }
    }

    public object SaveState()
    {
        Vector3[] actorsPosition = GetActorsPosition();
        float[,] currentActorsPosition = new float[_LMActors.Length,3];
        bool[] currentActorsActiveSelf = new bool[_LMActors.Length];
        int[] actorsCurrentConversationData = new int[_LMActors.Length];
        string[] actorsTag = new string[_LMActors.Length];

        for (int i = 0; i < _LMActors.Length; i++)
        {
            currentActorsPosition[i, 0] = actorsPosition[i].x;
            currentActorsPosition[i, 1] = actorsPosition[i].y;
            currentActorsPosition[i, 2] = actorsPosition[i].z;
            currentActorsActiveSelf[i] = _LMActors[i].activeSelf;
            actorsCurrentConversationData[i] = GetDialogueConversationID(i);
            actorsTag[i] = GetActorsTag(i);
        }

        return new SaveData()
        {
            savedCurrentActorsPosition = currentActorsPosition,
            savedCurrentActorsActiveSelf = currentActorsActiveSelf,
            actorsCurrentConversationData = actorsCurrentConversationData,
            actorsTag = actorsTag
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        Vector3 newActorsPosition = new Vector3();

        for (int i = 0; i < _LMActors.Length; i++)
        {
            newActorsPosition.x = saveData.savedCurrentActorsPosition[i, 0];
            newActorsPosition.y = saveData.savedCurrentActorsPosition[i, 1];
            newActorsPosition.z = saveData.savedCurrentActorsPosition[i, 2];
            
            _LMActors[i].transform.position = newActorsPosition;
            _LMActors[i].SetActive(saveData.savedCurrentActorsActiveSelf[i]);

            SetDialogueConversation(i, saveData.actorsCurrentConversationData[i]);

            _LMActors[i].tag = saveData.actorsTag[i];
        }
    }

    [Serializable]
    struct SaveData
    {
        public float[,] savedCurrentActorsPosition;
        public bool[] savedCurrentActorsActiveSelf;
        public int[] actorsCurrentConversationData;
        public string[] actorsTag;
    }
}
