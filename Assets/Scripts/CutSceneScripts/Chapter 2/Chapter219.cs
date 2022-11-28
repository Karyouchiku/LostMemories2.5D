using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Chapter219 : MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
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

    //[Header("Remove items in Inventory")]
    //public SOItemData[] items;
    //PlayerInventory inventory;

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
    public int actorIDToChange;
    public DialogueDatabase dialogueDatabase;
    public int convoID;

    void Start()
    {
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        //inventory = GameObject.Find("Inventory").GetComponent<PlayerInventory>();

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

        player.conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Listeners for enabling Controls
        dialogueModifier.AddListenersOnConversationEnd();//Adds the Listeners for enabling Controls
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01()
    {
        actors[actorID].tag = "NPC";
        actors[actorID].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;//Deactivating the trigger system
        ContinueMode(false);
        SetMinSubtitleSeconds(5);
        //SetActorStartingPosition(2, 8);
        StartMoving();
        MoveActor(3, 1, 1f);//Making Sure he stays in his starting position
        for (int i = 0; i < GameObjectChildrens.Length; i++)
        {
            GameObjectChildrens[i].SetActive(false);
        }
        /*
        //Activating other Objects
        for (int i = 0; i < otherGameObjects.Length; i++)
        {
            otherGameObjects[i].SetActive(true);
        }
        */
        GetComponent<ItemFromNPC>().RemoveItem();
    }
    public void ForDE76()
    {
        ContinueMode(true);
    }
    public void forDE108_107_85_127()//papasok si Goon
    {
        SetActorStartingPosition(19, 8);
        MoveActor(19, 2, 0.4f);
        actors[19].tag = "NPC";
        ContinueMode(false);
    }
    public void ForDe86()
    {
        ContinueMode(true);
    }
    public void ForDE100()//aalis na si randy
    {
        for (int i = 0; i < GameObjectChildrens.Length; i++)
        {
            GameObjectChildrens[i].SetActive(false);
        }
        ContinueMode(false);
        MoveActor(3, 4, 0.4f);
        actors[19].tag = "InteractableNPC";
    }
    public void ForDE109()//aalis na rin si goon
    {
        ContinueMode(true);
        MoveActor(19, 6, 0.4f);
    }
    public void ForDE114()
    {
        ContinueMode(false);
    }
    public void ForDE116()//Last Dialogue
    {
        ContinueMode(false);
        ResetActorPositionToOriginal(19);
        ResetActorPositionToOriginal(3);
        otherGameObjects[0].GetComponent<LockInteractableDoors>().isUnlocked = false;
        otherGameObjects[1].GetComponent<LockInteractableDoors>().isUnlocked = true;
        otherGameObjects[0].tag = "InteractableObject";
        IQuest.SetQuest("Find your way out to escape");
        IObjectives.SetObjective1("Find your confiscated things before you leave");
        EndingScene();
    }
    //END OF ForDE METHODS

    //MY SHORCUT METHODS
    void ChangeActorDialogue(int actorIDToChange, int convoID)//Use this for Interaction of NPC not for OnTriggerCollision
    {
        if (useDialogueManager)
        {
            actors[actorIDToChange].gameObject.tag = "InteractableNPC";
            actors[actorIDToChange].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.OnUse;
            actors[actorIDToChange].GetComponent<DialogueSystemTrigger>().conversation = dialogueDatabase.GetConversation(convoID).Title;
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
        actors[actorID].tag = "Untagged";
        actors[actorID].transform.position = lmActors.orginalActorLocations[actorID];
        targetLocation[actorID] = lmActors.orginalActorLocations[actorID];
        actors[actorID].GetComponent<CharacterAnimation>().ResetAnimation();
        actors[actorID].SetActive(false);
    }
    public PuzzleCh2 puzzle;
    public void EndingScene()
    {
        thisSceneDone = true;
        DisableChilds();
        puzzle.StartThisPuzzle();
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
        //targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
        
        MoveActor(actorID, locationID);
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