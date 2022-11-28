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
        //GOOD ENDING
    }
    IEnumerator ForDE13Coroutine()
    {
        yield return new WaitForSeconds(8f);
        DisableChilds();
        //menu.BackToMainMenu();

        //AFTER WORDS
        EndingAfterWords.SetActive(true);
        AfterWords("Adoption is a sensitive issue but it does not mean that people should not be open about it.", 7);
        yield return new WaitForSeconds(8f);
        AfterWords("Adoption affects the adopted child and the adoptive parents.", 4);
        yield return new WaitForSeconds(5f);
        AfterWords("Based on research, adoption stigma or things like hiding the adoption, looking down on adoptive families, and any negative connotations about adoption emotionally impacts both the child and the parent.", 16);
        yield return new WaitForSeconds(17f);
        AfterWords("This game was created to help create a society that is understanding and supportive of adoptive families.", 7);
        yield return new WaitForSeconds(8f);
        AfterWords("This game showed the different outcomes of adoption stigma like the poor handling of adoptive parents and negatively affecting the emotions of an adopted child.", 10);
        yield return new WaitForSeconds(11f);
        AfterWords("Adopted children or people needs support and understanding of everyone. Otherwise, they might \find themselves hard to trust other people which eventually affects their lives.", 11);
        yield return new WaitForSeconds(12f);
        AfterWords("It seems like you got the good ending. You have successfully created the correct decisions about the different factors that affects adoption stigma. We, the developers, would like to congratulate you for being an understanding and supportive to adoptive children based on your decisions. We hope you continue this attitude towards adoptive families to help create a society supportive of adoptive families.", 25);
        yield return new WaitForSeconds(26f);
        AfterWords("You may play the game again to find out about the different ending.", 5);
        yield return new WaitForSeconds(6f);
        AfterWords("Thanks for playing!", 4);
        yield return new WaitForSeconds(5f);
        menu.BackToMainMenu();

    }
    #endregion

    #region MY SHORCUT METHODS
    void AfterWords(string msg, float readTime)
    {
        StartCoroutine(AfterWordsCoroutine(msg, readTime));
    }
    IEnumerator AfterWordsCoroutine(string msg, float readTime)
    {
        EndingAfterWordsAnim.SetTrigger("FadeIn");
        EndingAfterWordsText.text = "msg";
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