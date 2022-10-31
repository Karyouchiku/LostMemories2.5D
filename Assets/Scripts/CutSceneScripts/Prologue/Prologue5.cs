using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prologue5 : MonoBehaviour, CutScenes, ISaveable
{
    public bool thisSceneDone;
    public bool startThisScene;
    [Header("Disable object and Scripts")]
    public GameObject inGameUI;
    public GameObject player;

    //[Header("Initial Data")]
    DialogueSystemController dialogueSystemController;
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

    BlackTransitioning transition;
    void Start()
    {
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
                DisableChilds();
            }
        }
    }
    void DisableChilds()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].gameObject.SetActive(false);
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

    public void StartMoving()
    {
        startThisScene = true;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 3;
        startMove[0] = true;
        startMove[1] = true;
        targetLocation[0] = locations[0].position;
        targetLocation[1] = locations[1].position;


        //player.GetComponent<DialogueSystemEvents>().conversationEvents.onConversationEnd.RemoveAllListeners();
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01_02_03()
    {
        actors[1].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;
        locations[0].GetComponent<BoxCollider>().enabled = false;
    }
    public void ForDE04()
    {
        targetLocation[1] = locations[2].position;
    }

    public void ForDE08_17_18_21_38()
    {
        targetLocation[0] = locations[3].position;
        ActorsMoveSpeed[0] = 0.4f;
    }

    //END OF ForDE METHODS
    public DialogueModifier dialogueModifier;
    public void EnterDoor()
    {
        //dialogueModifier.AddListenersOnConversationEnd();
    }


    public void EndingScene()
    {
        thisSceneDone = true;
    }
    public void ChangeLocation(int actorID, int locationID)
    {
        locations[locationID].gameObject.SetActive(true);
        targetLocation[actorID] = locations[locationID].position;
    }


    public void LocationCheck()
    {
        player.GetComponent<CharacterAnimation>().ResetAnimation();
        EndingScene();
    }


    public object SaveState()
    {
        return new SaveData()
        {
            thisSceneDone = this.thisSceneDone,
            startThisScene = this.startThisScene
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.thisSceneDone = saveData.thisSceneDone;
        this.startThisScene = saveData.startThisScene;
    }


    [Serializable]
    struct SaveData
    {
        public bool thisSceneDone;
        public bool startThisScene;
    }
}