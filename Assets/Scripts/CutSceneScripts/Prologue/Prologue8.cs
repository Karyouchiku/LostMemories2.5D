using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prologue8 : MonoBehaviour, CutScenes, ISaveable
{
    [Header("Disable object and Scripts")]
    public GameObject inGameUI;
    public GameObject player;

    //[Header("Initial Data")]
    DialogueSystemController dialogueSystemController;
    public bool thisSceneDone;
    bool[] startMove;

    [Header("Portal Doors Involved")]
    public PortalDoor[] doors;
    [Header("Actors: How many actors is present in this scene")]
    public GameObject[] actors;
    public float[] ActorsMoveSpeed;
    CharacterAnimation[] anim;
    [Header("Trigger Locations: Moving object 'goto' locations")]
    public Transform[] locations;
    Vector3[] targetLocation;
    Vector3 animVec;
    [Header("Other GameObjects")]
    public GameObject[] otherGameObjects;

    BlackTransitioning transition;
    DialogueModifier dialogueModifier;
    void Start()
    {
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        startMove = new bool[actors.Length];
        targetLocation = new Vector3[actors.Length];
        anim = new CharacterAnimation[actors.Length];
        transition = transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        for (int i = 0; i < actors.Length; i++)
        {
            anim[i] = actors[i].GetComponent<CharacterAnimation>();
        }
    }
    bool startThisScene;
    void Update()
    {
        if (startThisScene)
        {
            if (!thisSceneDone)
            {
                for (int i = 0; i < startMove.Length; i++)
                {
                    MoveCharacter(startMove[i], actors[i], anim[i], targetLocation[i], ActorsMoveSpeed[i]);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void MoveCharacter(bool startMove, GameObject actor, CharacterAnimation pAnim, Vector3 target, float mSpeed)
    {
        if (startMove)
        {
            animVec = target - actor.transform.position;
            target.y = actor.transform.position.y;
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, target, mSpeed * Time.deltaTime);
            pAnim.moveX = animVec.x;
            pAnim.moveZ = animVec.z;
        }
    }

    //START OF ALL EVENT METHODS

    //Calls from TriggerCutscene 
    public void StartMoving()
    {

        //player.GetComponent<DialogueSystemEvents>().conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Comment to activate this line
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01()
    {
        //startThisScene = true;
        actors[1].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 3;
    }
    

    //END OF ForDE METHODS

    public void EndingScene()
    {
        thisSceneDone = true;
    }

    //Calls from AutoEnterDoor
    public void EnterDoor()
    {
        dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        EndingScene();
    }

    //Calls from LocationChanger
    public void ChangeLocation(int i)
    {
    }

    //Calls from LocationChecker
    public void LocationCheck()
    {
    }

    //END OF ALL EVENT METHODS


    public object SaveState()
    {
        return new SaveData()
        {
            thisSceneDone = this.thisSceneDone,
            startMove = this.startMove
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.thisSceneDone = saveData.thisSceneDone;
        this.startMove = saveData.startMove;
    }


    [Serializable]
    struct SaveData
    {
        public bool thisSceneDone;
        public bool[] startMove;
    }
}
