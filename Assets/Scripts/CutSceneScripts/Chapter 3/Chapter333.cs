using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Chapter333 : MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
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
    bool oneTimeSwitch;
    WorldActiveSaveState renderWorld;
    void Start()
    {
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        player = GameObject.Find("Player").GetComponent<DialogueSystemEvents>();
        saveSystem = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SaveSystem>();
        renderWorld = GameObject.Find("World").GetComponent<WorldActiveSaveState>();

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
                    oneTimeSwitch = true;
                    OtherGOSwitch(true);
                }
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
    public void StartMoving()
    {
        startThisScene = true;
    }
    #endregion
    #region ForDE METHODS
    public void ForDE01()
    {
        StartMoving();
        EnableListenersOnConvoEnd(false);
        DisableInteractable();
        ContinueMode(false);
        SetMinSubtitleSeconds(4);
        //SetActorStartingPosition(5, 8);
        //ShadowyActor(5, true);
        DisableChilds();
        //ChangeActorDialogue();
    }
    public void ForDE03()
    {
        transition.ManualTransitionOFF();
        ContinueMode(true);
        MoveActor(20, 3, 0.5f);
    }
    public void ForDE38()
    {
        //transition.ManualTransitionON();
        GetComponent<ItemFromNPC>().RemoveItem();
    }
    public void ForDE44()
    {
        ContinueMode(false);
        transition.ManualTransitionON();
    }
    public void ForDE45()
    {
        ContinueMode(true);
        ResetActorPositionToOriginal(20);
    }
    public void ForDE48()
    {
        ContinueMode(false);
        transition.ManualTransitionON();
        MoveActor(0, 11, 1f);
    }
    public void ForDE49()
    {
        //20sec until DE 54
        renderWorld.RenderWorlds(false, 7);
        ResetActorPositionToOriginal(20);
        StartCoroutine(ForDE49Coroutine());
    }
    IEnumerator ForDE49Coroutine()
    {
        //First Run
        TransitioningToOtherPlaces(12, 13, 1f);
        transition.ManualTransitionOFF();
        yield return new WaitForSeconds(4f);
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1f);
        //Sec 5
        //Second Run
        TransitioningToOtherPlaces(14, 15, 1f);
        transition.ManualTransitionOFF();
        yield return new WaitForSeconds(4f);
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1f);
        //Sec 10
        //third Run
        TransitioningToOtherPlaces(16, 17, 0.7f);
        transition.ManualTransitionOFF();
        yield return new WaitForSeconds(4f);
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1f);
        //Sec 15
        //Forth Run
        TransitioningToOtherPlaces(18, 19, 0.4f);      
        transition.ManualTransitionOFF();
        //Sec <20
    }
    public void ForDE54()
    {
        MoveActor(0, 19);
    }
    public void ForDE55()
    {
        transition.ManualTransitionON();
    }

    public void ForDE56()//back to hospital
    {
        renderWorld.RenderWorlds(false, 12);
        TransitioningToOtherPlaces(20);
        SetActorStartingPosition(6, 6);
        SetActorStartingPosition(20, 24);
    }
    public void ForDE60()//Doctor out
    {
        MoveActor(20, 25, 0.4f);
    }
    public void ForDE61()//Oh there you are!
    {
        transition.ManualTransitionOFF();
    }

    public void ForDE62()
    {
        MoveActor(0, 21, 0.4f);
    }
    public void ForDE85()
    {
        transition.ManualTransitionON();
    }
    public void ForDE86()
    {
        ResetActorPositionToOriginal(20);
        SetActorStartingPosition(0, 22);
        SetActorStartingPosition(6, 23);
    }
    public void ForDE87()
    {
        transition.ManualTransitionOFF();
    }
    public void ForDE93()
    {
        MoveActor(0, 31, 0.4f);
        MoveActor(6, 32, 0.4f);
        StartCoroutine(ForDE93Coroutine());
    }
    IEnumerator ForDE93Coroutine() 
    {
        yield return new WaitForSeconds(2f);
        transition.ManualTransitionON();
    }
    
    public void ForDE104()
    {
        ContinueMode(true);
    }
    
    public void ForDE59()
    {
        ContinueMode(true);
        SetActorStartingPosition(6, 6);
        MoveActor(6, 6, 0.1f);
        MoveActor(0, 7, 0.4f);
        transition.ManualTransitionOFF();
    }
    public void ForDE137()
    {
        MoveActor(6, 33, 0.4f);
        StartCoroutine(ForDE137Coroutine());
    }
    IEnumerator ForDE137Coroutine()
    {
        yield return new WaitForSeconds(3f);
        MoveActor(6, 34, 0.4f);
    }
    public void ForDE171()
    {
        transition.ManualTransitionON();
    }
    public void ForDE95()//Bad Ending
    {
        ContinueMode(false);
        Door(0);
        EndingScene();
    }
    public void ForDE179()//Good Ending
    {
        ContinueMode(false);
        Door(0);
        EndingScene();
    }
    
    /*
    public void ForDE05()
    {
        //OtherGOSwitch(true, 0);
        ContinueMode(false);
        IQuest.SetQuest("Return to Flor's House");
        EndingScene();
        Checkpoint();
    }
    */

    public void ForDE_ThatGivesItem()
    {
        GetComponent<ItemFromNPC>().GiveItem();
    }
    public void ForDE_ThatRemovesItem()
    {
        GetComponent<ItemFromNPC>().RemoveItem();
    }
    #endregion

    #region MY SHORCUT METHODS
    void TransitioningToOtherPlaces(int locationID)
    {
        StartCoroutine(TransitioningToOtherPlacesCoroutine(locationID));
    }
    IEnumerator TransitioningToOtherPlacesCoroutine(int locationID)
    {
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1f);
        player.GetComponent<CharacterController>().enabled = false;
        SetActorStartingPosition(0, locationID);
        player.GetComponent<CharacterController>().enabled = true;
    }
    void TransitioningToOtherPlaces(int locationID, int moveToLocationID, float moveSpeed)
    {
        player.GetComponent<CharacterController>().enabled = false;
        SetActorStartingPosition(0, locationID);
        player.GetComponent<CharacterController>().enabled = true;
        MoveActor(0, moveToLocationID, moveSpeed);

    }

    void EnableListenersOnConvoEnd(bool enable)
    {
        player.conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Listeners for enabling Controls
        if (enable)
        {
            dialogueModifier.AddListenersOnConversationEnd();//Adds the Listeners for enabling Controls
        }
    }
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
    void Door(int doorID)
    {
        doors[doorID].Interact();
    }
    void SetActorStartingPosition(int actorID, int locationID)
    {
        actors[actorID].SetActive(true);
        actors[actorID].transform.position = GameObjectChildrens[locationID].transform.position;
        MoveActor(actorID, locationID);
    }
    void ShadowyActor(int actorID, bool isShadow)
    {
        Color color = isShadow ? Color.black : Color.white;
        actors[actorID].GetComponentInChildren<SpriteRenderer>().color = color;
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
    #endregion
    #region CutScenes Methods For External Callers
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
    }

    #endregion

    #region ISaveable Methods
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
    #endregion
}