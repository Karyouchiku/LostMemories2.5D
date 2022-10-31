using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prologue7 : MonoBehaviour, CutScenes, ISaveable
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
    public void ForDE39()
    {
        startThisScene = true;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 3;
        //actors[1].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;
        otherGameObjects[1].SetActive(false);
    }
    public void ForDE41()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        transition.ManualTransitionOFF();

    }
    public void ForDE45()
    {
        //Teleports Takita to starting point
        targetLocation[1] = locations[3].position;
        targetLocation[1].y = actors[1].transform.position.y;
        actors[1].transform.position = targetLocation[1];
        actors[1].SetActive(true);
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        startMove[1] = true;
        targetLocation[1] = locations[0].position;
    }
    public void ForDE46()
    {
        targetLocation[1] = locations[1].position;
    }
    public void ForDE47_50()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
    }
    public void ForDE55_56()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        StartCoroutine(ForDE55_56Coroutine());
    }
    IEnumerator ForDE55_56Coroutine()
    {
        targetLocation[1] = locations[0].position;
        yield return new WaitForSeconds(1);
        targetLocation[1] = locations[2].position;
        yield return new WaitForSeconds(3);
        actors[1].SetActive(false);
    }
    public void ForDE58()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
    }
    public void ForDE61()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;

        EnterDoor();
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
    public void ChangeLocation(int actorID, int locationID)
    {
        locations[locationID].gameObject.SetActive(true);
        targetLocation[actorID] = locations[locationID].position;
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