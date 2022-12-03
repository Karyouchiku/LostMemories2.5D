using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Chapter437 : MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
{
    #region Starting Codes
    BackgroundMusicScript bgm;
    EndingMessage msgs;
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

    public GameObject EndingAfterWords;
    Animator EndingAfterWordsAnim;
    TMP_Text EndingAfterWordsText;

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
    IngameMenuScript menu;
    WorldActiveSaveState renderWorld;
    void Start()
    {
        msgs = new EndingMessage();
        bgm = GameObject.Find("BGM").GetComponent<BackgroundMusicScript>();
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        player = GameObject.Find("Player").GetComponent<DialogueSystemEvents>();
        saveSystem = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SaveSystem>();
        renderWorld = GameObject.Find("World").GetComponent<WorldActiveSaveState>();
        menu = GameObject.FindGameObjectWithTag("Canvas").GetComponent<IngameMenuScript>();
        EndingAfterWordsAnim = EndingAfterWords.GetComponent<Animator>();
        EndingAfterWordsText = EndingAfterWords.GetComponentInChildren<TMP_Text>();

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
        bgm.ChangeBGM(19);
        StartMoving();
        EnableListenersOnConvoEnd(false);
        DisableInteractable();
        ContinueMode(false);
        SetMinSubtitleSeconds(4);
        SetActorStartingPosition(8, 2);
        SetActorStartingPosition(9, 3);
        SetActorStartingPosition(10, 4);
        DisableChilds();
    }
    public void ForDE07()
    {
        transition.ManualTransitionOFF();
        MoveActor(0, 1, 0.5f);
    }
    public void ForDE12()
    {
        transition.ManualTransitionON();
    }
    public void ForDE13()
    {
        StartCoroutine(ForDE13Coroutine());
        EndingScene();
        //GOOD ENDING
    }
    IEnumerator ForDE13Coroutine()
    {
        yield return new WaitForSeconds(5f);
        DisableChilds();
        bgm.ChangeBGM(3);
        //AFTER WORDS
        EndingAfterWords.SetActive(true);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[0], 9));
        //AfterWords(msgs.afterWordsMesseges[0], 9);
        yield return new WaitForSeconds(10.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[1], 6));
        //AfterWords(msgs.afterWordsMesseges[1], 6);
        yield return new WaitForSeconds(7.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[2], 18));
        //AfterWords(msgs.afterWordsMesseges[2], 18);
        yield return new WaitForSeconds(19.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[3], 9));
        //AfterWords(msgs.afterWordsMesseges[3], 9);
        yield return new WaitForSeconds(10.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[4], 12));
        //AfterWords(msgs.afterWordsMesseges[4], 12);
        yield return new WaitForSeconds(13.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[5], 13));
        //AfterWords(msgs.afterWordsMesseges[5], 13);
        yield return new WaitForSeconds(14.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[6], 27));
        //AfterWords(msgs.afterWordsMesseges[7], 27);//Unique
        yield return new WaitForSeconds(28.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[8], 7));
        //AfterWords(msgs.afterWordsMesseges[8], 7);
        yield return new WaitForSeconds(8.5f);

        StartCoroutine(AfterWordsCoroutine(msgs.afterWordsMesseges[9], 6));
        //AfterWords(msgs.afterWordsMesseges[9], 6);
        yield return new WaitForSeconds(7.5f);

        bgm.ChangeBGM();
        menu.BackToMainMenu();

    }
    #endregion
    #region MY SHORCUT METHODS
    /* 
    void AfterWords(string msg, float readTime)
    {
        StartCoroutine(AfterWordsCoroutine(msg, readTime));
    }
    */
    IEnumerator AfterWordsCoroutine(string msg, float readTime)
    {
        EndingAfterWordsAnim.SetTrigger("FadeIn");
        EndingAfterWordsText.text = msg;
        yield return new WaitForSeconds(readTime);
        EndingAfterWordsAnim.ResetTrigger("FadeIn");
        EndingAfterWordsAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        EndingAfterWordsAnim.ResetTrigger("FadeOut");
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