using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class Chapter115: MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
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
    TMP_Text questText;
    [Header("Actor to Trigger Dialogue")]
    public int actorID;
    [Header("Scene Dialogue Changer")]
    public bool useDialogyeManager;
    public DialogueDatabase dialogueDatabase;
    public int convoID;

    void Start()
    {
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        transition = transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        questText = GameObject.Find("Quest Text").GetComponent<TMP_Text>();
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

        dialogueModifier.AddListenersOnConversationEnd();//Adds the Listeners for enabling Controls
        //player.conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Listeners for enabling Controls
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01()
    {
        actors[actorID].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;//Deactivating the trigger system
        actors[actorID].tag = "Untagged";
        //dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        ContinueMode(false);
        //SetMinSubtitleSeconds(3);
        StartMoving();
        SetActorStartingPosition(3, 1);
        
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
        MoveActor(3, 2, 1f);
    }
    public void ForDE02()
    {
        ContinueMode(true);
        MoveActor(3, 3);
        player.GetComponent<FlashlightControls>().FLSwitch(false);
    }

    public void ForDE20()
    {
        MoveActor(3, 1);
        MoveActor(0, 4, 0.8f);
        StartCoroutine(ForDE20Coroutine());
    }

    IEnumerator ForDE20Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1);
        GetComponent<ItemFromNPC>().GiveItem();
        for (int i = 0; i < otherGameObjects.Length; i++)
        {
            otherGameObjects[i].SetActive(true);
        }
        SetActorStartingPosition(15, 7);
        
        yield return new WaitForSeconds(1);
        questText.text = "Meet the receiver behind the store from the otherside of the road";
        Door(0);
        EndingScene();
    }
    //END OF ForDE METHODS

    //MY SHORCUT METHODS
    void Door(int doorID)
    {
        doors[doorID].Interact();
    }
    void ChangeActorDialogue(int actorID, int convoID)//Use this for Interaction of NPC not for OnTriggerCollision
    {
        if (useDialogyeManager)
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
        player.GetComponent<FlashlightControls>().FLSwitch(true);
    }

    //Calls from AutoEnterDoor
    public void EnterDoor()
    {
        //dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        player.GetComponent<FlashlightControls>().FLSwitch(true);
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