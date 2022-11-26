using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Chapter329 : MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
{
    #region Starting Codes
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

    //[Header("Disable object and Scripts")]
    DialogueSystemEvents player;

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
    public int actorIDToChange;
    public int convoID;
    public DialogueDatabase dialogueDatabase;
    SaveSystem saveSystem;
    int forNextConvo = 0;
    bool oneTimeSwitch;
    void Start()
    {
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        player = GameObject.Find("Player").GetComponent<DialogueSystemEvents>();
        saveSystem = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SaveSystem>();

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
                if (!oneTimeSwitch)
                {
                    if (forNextConvo == 1)
                    {
                        OtherGOSwitch(true, 0);
                    }
                    else if (forNextConvo == 2)
                    {
                        OtherGOSwitch(true, 1);
                    }
                    oneTimeSwitch = true;
                }
            }
        }
    }

    void DisableChilds()
    {
        foreach (GameObject go in GameObjectChildrens)
        {
            go.SetActive(false);
        }
    }
    void OtherGOSwitch(bool turn)
    {
        foreach (GameObject go in otherGameObjects)
        {
            go.SetActive(turn);
        }
    }
    void OtherGOSwitch(bool turn, int id)
    {
        otherGameObjects[id].SetActive(turn);
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
    #endregion
    
    //START OF ALL EVENT METHODS

    //Calls from TriggerCutscene 
    public void StartMoving()
    {
        startThisScene = true;
        player.conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Listeners for enabling Controls
        //dialogueModifier.AddListenersOnConversationEnd();//Adds the Listeners for enabling Controls
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01()
    {
        StartMoving();
        DisableInteractable();
        ContinueMode(true);
        SetMinSubtitleSeconds(4);
        //SetActorStartingPosition(6, 1);
        DisableChilds();
        //ChangeActorDialogue();

    }
    public void ForDE06()
    {
        StartCoroutine(ForDE06Coroutine());
    }
    IEnumerator ForDE06Coroutine()
    {
        MoveActor(6, 0, 0.5f);
        yield return new WaitForSeconds(1f);
        MoveActor(0, 0, 0.5f);
    }
    public void ForDE08()
    {
        ContinueMode(false);
        StartCoroutine(ForDE08Coroutine());
    }
    IEnumerator ForDE08Coroutine()
    {
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1f);
        SetActorStartingPosition(0, 1);
        SetActorStartingPosition(6, 2);
        MoveActor(0, 1);
        MoveActor(6, 2);
    }
    public void ForDE33()
    {
        ContinueMode(false);
        MoveActor(0, 3, 0.4f);
        StartCoroutine(ForDE33Coroutine());
    }
    IEnumerator ForDE33Coroutine()
    {
        yield return new WaitForSeconds(1f);
        MoveActor(6, 4, 0.6f);
    }
    public void ForDE35()
    {
        ContinueMode(true);

    }
    public void ForDE61()
    {
        ContinueMode(false);
        MoveActor(6, 5, 0.7f);
        StartCoroutine(ForDE61Coroutine());
    }
    IEnumerator ForDE61Coroutine()
    {
        yield return new WaitForSeconds(6f);
        ResetActorPositionToOriginal(6);

    }
    public void ForDE64()//Failed to Convince Paloma
    {
        ContinueMode(false);
        MoveActor(0, 9, 0.5f);
        forNextConvo = 2;
    }

    public void ForDE78()
    {
        ContinueMode(false);
        MoveActor(6, 5, 0.4f);
        StartCoroutine(ForDE78Coroutine());
    }
    IEnumerator ForDE78Coroutine()
    {
        yield return new WaitForSeconds(1f);
        transition.ManualTransitionON();
    }
    public void ForDE79()
    {
        StartCoroutine(ForDE79Coroutine());
    }
    IEnumerator ForDE79Coroutine()//Success to Convince Paloma
    {
        SetActorStartingPosition(0, 7);
        MoveActor(0, 7);
        SetActorStartingPosition(6, 8);
        MoveActor(6, 8);

        yield return new WaitForSeconds(1f);
        transition.ManualTransitionOFF();
        forNextConvo = 1;
        
        EndingScene();
    }
    public void ForDE107()
    {
        ContinueMode(true);
        transition.ManualTransitionOFF();
    }

    public void ForDE_ThatGivesThePicture()
    {
        GetComponent<ItemFromNPC>().RemoveItem();
    }

    /*
    public void ForDE03()//From CH3 C28
    {
        //OtherGOSwitch(true, 0);
        IQuest.SetQuest("Ask someone for help to find the building");
        Checkpoint();
        EndingScene();
    }
    */
    //END OF ForDE METHODS

    //MY SHORCUT METHODS
    void Checkpoint()
    {
        saveSystem.Save(5);
    }
    void DisableInteractable()
    {
        actors[actorID].tag = "NPC";
        actors[actorID].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;//Deactivating the trigger system
    }

    void ChangeActorDialogue()//Use this for Interaction of NPC not for OnTriggerCollision
    {
        actors[actorIDToChange].gameObject.tag = "InteractableNPC";
        actors[actorIDToChange].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.OnUse;
        actors[actorIDToChange].GetComponent<DialogueSystemTrigger>().conversation = dialogueDatabase.GetConversation(convoID).Title;
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
        actors[actorID].GetComponent<CharacterAnimation>().ResetAnimation();
        actors[actorID].SetActive(false);
    }
    public void EndingScene()
    {
        thisSceneDone = true;
        //player.GetComponent<FlashlightControls>().FLSwitch(false);
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
        //GameObjectChildrens[locationID].SetActive(true);
        //targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
        
        MoveActor(actorID, locationID);
    }

    //Calls from LocationChecker
    public void LocationCheck()
    {
        EndingScene();
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