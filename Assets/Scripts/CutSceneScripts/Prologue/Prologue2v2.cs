using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prologue2v2 : MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
{
    //important to be saved
    public bool thisSceneDone;
    public bool startThisScene;

    //Initial Data
    DialogueSystemController dialogueSystemController;
    bool[] startMove;
    GameObject[] actors;
    LMActors lmActors;
    float[] ActorsMoveSpeed;
    CharacterAnimation[] anim;
    BlackTransitioning transition;
    DialogueModifier dialogueModifier;

    [Header("Disable object and Scripts")]
    public DialogueSystemEvents player;

    [Header("Portal Doors Involved")]
    public PortalDoor[] doors;


    [Header("Use for Locations: Moving object to these locations")]
    public GameObject[] GameObjectChildrens;
    Vector3[] targetLocation;
    Vector3 animVec;

    [Header("For Other GameObjects involved")]
    public GameObject[] otherGameObjects;

    [Header("Actor to Trigger Dialogue")]
    public int actorID;
    [Header("Scene Dialogue Changer")]
    public bool useDialogueManager;
    public DialogueDatabase dialogueDatabase;
    public int convoID;

    void Start()
    {
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        transition = transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();

        actors = lmActors._LMActors;
        startMove = new bool[actors.Length];
        ActorsMoveSpeed = new float[actors.Length];
        targetLocation = new Vector3[actors.Length];
        anim = new CharacterAnimation[actors.Length];
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
        for (int i = 0; i < GameObjectChildrens.Length; i++)
        {
            GameObjectChildrens[i].SetActive(false);
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
        startThisScene = true;

        //dialogueModifier.AddListenersOnConversationEnd();//Adds the Listeners for enabling Controls
        //player.conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Listeners for enabling Controls
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01()
    {
        actors[actorID].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;//Deactivating the trigger system
        StartMoving();
        //dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        ContinueMode(true);
        SetMinSubtitleSeconds(3);
        //SetActorStartingPosition(2, 8);
        MoveActor(0, 0, 1);
        for (int i = 0; i < GameObjectChildrens.Length; i++)
        {
            GameObjectChildrens[i].SetActive(false);
        }

        //Activating other Objects
        for (int i = 0; i < otherGameObjects.Length; i++)
        {
            otherGameObjects[i].SetActive(true);
        }

    }

    public void ForDE02()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        ContinueMode(true);
        MoveActor(8, 1, 0.7f);
        MoveActor(9, 2, 0.7f);
        MoveActor(10, 3, 0.7f);
        
    }
    public void ForDE03_08_13()
    {
        ContinueMode(true);
    }
    public void ForDE17()
    {
        ContinueMode(false);
        SetMinSubtitleSeconds(1.5f);
        MoveActor(9, 4, 1.3f);
        StartCoroutine(ForDE17Coroutine());
    }
    IEnumerator ForDE17Coroutine()
    {
        yield return new WaitForSeconds(0.3f);
        MoveActor(9, 2, 1.3f);
        ContinueMode(true);
        SetMinSubtitleSeconds(4);
    }
    public void ForDE29()
    {
        ContinueMode(false);
        transition.ManualTransitionON();
        MoveActor(8, 8, 0.7f);
        MoveActor(9, 9, 0.7f);
        MoveActor(10, 10, 0.7f);
    }

    public void ForDE33()
    {
        ContinueMode(false);
        SetMinSubtitleSeconds(2f);
        StartCoroutine(ForDE33Coroutine());
    }
    IEnumerator ForDE33Coroutine()
    {
        MoveActor(8, 5, 2f);
        MoveActor(9, 6, 2f);
        MoveActor(10, 7, 2f);
        yield return new WaitForSeconds(3f);
        ResetActorPositionToOriginal(8);
        ResetActorPositionToOriginal(9);
        ResetActorPositionToOriginal(10);
        yield return null;
    }
    public void ForDE36()
    {
        ContinueMode(false);
        StartCoroutine(ForDE36Coroutine());
    }
    IEnumerator ForDE36Coroutine()
    {
        MoveActor(8, 5, 0.7f);
        MoveActor(9, 6, 0.7f);
        MoveActor(10, 7, 0.7f);
        
        yield return new WaitForSeconds(3f);
        ResetActorPositionToOriginal(8);
        ResetActorPositionToOriginal(9);
        ResetActorPositionToOriginal(10);
        yield return null;
    }
    public void ForDE38()
    {
        ContinueMode(true);
    }
    public void ForDE40()
    {
        ContinueMode(false);
        Door(0);
        EndingScene();
    }
    public void ForDE41()
    {
        MoveActor(0, 11, 0.5f);
        SetActorStartingPosition(2, 14);
        MoveActor(2, 12, 1.4f);
        
    }
    public void ForDE43()
    {
        ContinueMode(false);
        SetMinSubtitleSeconds(2);
        Door(1);
        StartCoroutine(SettingActorStartingPoint());
        EndingScene();
    }
    public void ForDE44()
    {
        SetMinSubtitleSeconds(2);
        Door(1);
        StartCoroutine(SettingActorStartingPoint());
        EndingScene();
    }
    IEnumerator SettingActorStartingPoint()
    {
        yield return new WaitForSeconds(1);
        SetActorStartingPosition(2, 15);
    }

    //END OF ForDE METHODS

    //MY SHORCUT METHODS
    void ChangeActorDialogue(int actorID, int convoID)//Use this for Interaction of NPC not for OnTriggerCollision
    {
        if (useDialogueManager)
        {
            actors[actorID].gameObject.tag = "InteractableNPC";
            actors[actorID].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.OnUse;
            actors[actorID].GetComponent<DialogueSystemTrigger>().conversation = dialogueDatabase.GetConversation(convoID).Title;
        }
    }
    void ContinueMode(bool isOptional)
    {
        if (isOptional)
        {
            dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        }
        else
        {
            dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        }
    }
    void SetMinSubtitleSeconds(float sec)
    {
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = sec;
    }
    void Door(int doorID)
    {
        doors[doorID].Interact();
    }

    void SetActorStartingPosition(int actorID, int locationID)
    {
        actors[actorID].SetActive(true);
        actors[actorID].transform.position = GameObjectChildrens[locationID].transform.position;
    }
    void MoveActor(int actorID, int locationID)
    {
        startMove[actorID] = true;
        actors[actorID].SetActive(true);
        GameObjectChildrens[locationID].SetActive(true);
        targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
    }
    void MoveActor(int actorID, int locationID, float moveSpeed)
    {
        startMove[actorID] = true;
        actors[actorID].SetActive(true);
        GameObjectChildrens[locationID].SetActive(true);
        ActorsMoveSpeed[actorID] = moveSpeed;
        targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
    }
    void ResetActorPositionToOriginal(int actorID)
    {
        actors[actorID].transform.position = lmActors.orginalActorLocations[actorID];
        targetLocation[actorID] = lmActors.orginalActorLocations[actorID];
        actors[actorID].SetActive(false);
    }
    public void EndingScene()
    {
        thisSceneDone = true;
    }

    //Calls from AutoEnterDoor
    public void EnterDoor()
    {
        //dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        EndingScene();
    }

    //Calls from LocationChanger
    public void ChangeLocation(int actorID, int locationID)
    {
        GameObjectChildrens[locationID].SetActive(true);
        targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
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

    public void ChangeLocation(int actorID, int locationID, float moveSpeed)
    {
        throw new NotImplementedException();
    }

    [Serializable]
    struct SaveData
    {
        public bool thisSceneDone;
        public bool startThisScene;
    }
}