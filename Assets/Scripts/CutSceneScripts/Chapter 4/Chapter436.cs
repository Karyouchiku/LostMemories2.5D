using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Chapter436 : MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
{
    #region Starting Codes
    BackgroundMusicScript bgm;
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
    WorldActiveSaveState renderWorld;
    void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<BackgroundMusicScript>();
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
                OtherGOSwitch(true);
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
        bgm.ChangeBGM(22);
        StartMoving();
        EnableListenersOnConvoEnd(false);
        DisableInteractable();
        ContinueMode(false);
        SetMinSubtitleSeconds(4);
        ShadowyActor(1, true);
        DisableChilds();
    }
    public void ForDE06()
    {
        ContinueMode(true);
        transition.ManualTransitionOFF();
    }
    public void ForDE07()
    {
        ShadowyActor(2, false);
    }

    public void ForDE31()
    {
        ContinueMode(false);
        transition.ManualTransitionON();
        MoveActor(0, 2, 1f);
        StartCoroutine(ForDE31Coroutine());
    }
    IEnumerator ForDE31Coroutine()
    {
        yield return new WaitForSeconds(1f);
        TransitioningToOtherPlaces(3);
    }
    public void ForDE33()
    {
        SetActorStartingPosition(7, 9);
        ShadowyActor(7, true);
        transition.ManualTransitionOFF();
        MoveActor(0, 4, 0.6f);
    }
    public void ForDE37()
    {
        MoveActor(0, 6, 1.2f);
    }
    public void ForDE41()
    {
        ContinueMode(true);
        ShadowyActor(7, false);
    }
    public void ForDE47()
    {
        ContinueMode(false);
        MoveActor(0, 10, 0.4f);
    }
    public void ForDE50()
    {
        bgm.ChangeBGM(15);
        ContinueMode(true);
        MoveActor(0, 11, 1f);
    }
    public void ForDE173()
    {
        ContinueMode(false);
        MoveActor(0, 12, 1f);
        transition.ManualTransitionON();
    }
    public void ForDE175()
    {
        TransitioningToOtherPlaces(2);
        transition.ManualTransitionOFF();
        ContinueMode(true);
        MoveActor(0, 13, 1f);
    }

    public void ForDE184()
    {
        bgm.ChangeBGM(21);
        ContinueMode(false);
        MoveActor(0, 12, 1f);
        transition.ManualTransitionON();
    }
    public void ForDE125()//Carol Entrance
    {
        bgm.ChangeBGM(13);
        SetActorStartingPosition(1, 14);
        MoveActor(1, 15, 0.5f);
    }
    public void ForDE127()
    {
        ContinueMode(true);
        ShadowyActor(1, false);
        MoveActor(0, 16, 1f);
    }
    public void ForDE146()
    {
        transition.ManualTransitionON();
    }
    public void ForDE98()
    {
        ContinueMode(false);
        transition.ManualTransitionON();
    }
    public void ForDE100()
    {
        ContinueMode(true);
        transition.ManualTransitionOFF();
        MoveActor(0, 13, 0.4f);
    }

    public void ForDE190()//Bad Ending
    {
        EndingTransition(18);
        EndingScene();
    }
    public void ForDE156()//Good Ending
    {
        renderWorld.RenderWorlds(true, 3);
        EndingTransition(17);
        EndingScene();
    }

    #endregion
    #region MY SHORCUT METHODS
    void EndingTransition(int locationID)
    {
        player.GetComponent<CharacterController>().enabled = false;
        SetActorStartingPosition(0, locationID);
        player.GetComponent<CharacterController>().enabled = true;
    }
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
        player.conversationEvents.onConversationEnd.RemoveAllListeners();
        if (enable)
        {
            dialogueModifier.AddListenersOnConversationEnd();
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
        OtherGOSwitch(true);
    }
    #endregion
    #region CutScenes Methods For External Callers
    //Calls from AutoEnterDoor
    public void EnterDoor()
    {
        EndingScene();
    }

    //Calls from LocationChanger
    public void ChangeLocation(int actorID, int locationID)
    {
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