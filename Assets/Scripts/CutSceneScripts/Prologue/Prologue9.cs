using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prologue9 : MonoBehaviour, CutScenes, ISaveable//Rename Class ***********************
{
    //important to be saved
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
    
    //[Header("Actors: How many actors is present in this scene")]
    GameObject[] actors;
    LMActors lmActors;
    float[] ActorsMoveSpeed;
    CharacterAnimation[] anim;
    
    [Header("Trigger Locations: Moving object 'goto' locations")]
    public Transform[] locations;
    Vector3[] targetLocation;
    Vector3 animVec;

    //[Header("Other GameObjects")]
    public GameObject[] otherGameObjects;

    BlackTransitioning transition;
    DialogueModifier dialogueModifier;
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
                otherGameObjects[2].SetActive(true);//Activating puzzle mode
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
        startThisScene = true;
        
        //dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        //player.GetComponent<DialogueSystemEvents>().conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Comment to activate this line
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01()
    {
        actors[2].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;//Deactivating the trigger system
        dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        ContinueMode(false);
        SetMinSubtitleSeconds(3);

        SetActorStartingPosition(2, 8);

        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < otherGameObjects.Length; i++)
        {
            otherGameObjects[i].SetActive(true);
        }
    }
    public void ForDE02()//Marisa at the door
    {
        ContinueMode(true);
        transition.ManualTransitionOFF();
    }
    public void ForDE03_04()//Burito opens the door
    {
        ContinueMode(false);
        MoveActor(0,0, 0.5f);

    }
    public void ForDE06()//Burito goes back to bed and marisa follows
    {
        ContinueMode(true);
        SetMinSubtitleSeconds(3);
        MoveActor(0, 1);
        MoveActor(2, 2, 0.5f);
    }
    public void ForDE20()//Marisa goes out
    {
        ContinueMode(false);
        MoveActor(2, 3, 0.8f);
        StartCoroutine(ForDE20Coroutine());
    }

    IEnumerator ForDE20Coroutine()
    {
        yield return new WaitForSeconds(6);
        ResetActorPositionToOriginal(2);
        //actors[1].SetActive(false); //this may throw an ERROR
        ContinueMode(true);
    }
    public void ForDE26()//Burito moves forward
    {
        MoveActor(0, 7, 1f);
    }
    
    public void ForDE29()//last dialogue
    {
        EndingScene();
    }


    //END OF ForDE METHODS

    //MY SHORCUT METHODS
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
        actors[actorID].transform.position = locations[locationID].position;
    }
    void MoveActor(int actorID, int locationID)
    {
        startMove[actorID] = true;
        locations[locationID].gameObject.SetActive(true);
        targetLocation[actorID] = locations[locationID].position;
    }
    void MoveActor(int actorID, int locationID, float moveSpeed)
    {
        startMove[actorID] = true;
        locations[locationID].gameObject.SetActive(true);
        ActorsMoveSpeed[actorID] = moveSpeed;
        targetLocation[actorID] = locations[locationID].position;
    }
    void ResetActorPositionToOriginal(int actorID)
    {
        actors[actorID].transform.position = lmActors.orginalActorLocations[actorID];
        targetLocation[actorID] = lmActors.orginalActorLocations[actorID];
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