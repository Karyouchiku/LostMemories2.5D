using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prologue8 : MonoBehaviour, CutScenes, ISaveable
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
    public bool DisableTAKITA;
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
        actors[1].SetActive(DisableTAKITA);
    }
    void DisableChilds()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].gameObject.SetActive(false);
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
        player.GetComponent<DialogueSystemEvents>().conversationEvents.onConversationEnd.RemoveAllListeners();//Remove the Comment to activate this line
    }
    // START CREATING ForDE METHODS HERE
    public void ForDE01()
    {
        StartMoving();
        actors[1].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 3;
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].gameObject.SetActive(false);
        }
    }
    public void ForDE13()
    {
        locations[0].gameObject.SetActive(true);
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        targetLocation[1] = locations[0].position;
        targetLocation[1].y = actors[1].transform.position.y;
        actors[1].transform.position = targetLocation[1];
        DisableTAKITA = true;

    }
    public void ForDE14()
    {
        locations[1].gameObject.SetActive(true);
        startMove[1] = true;
        targetLocation[1] = locations[1].position;
    }
    public void ForDE16()
    {
        locations[2].gameObject.SetActive(true);
        startMove[0] = true;
        targetLocation[0] = locations[2].position;
    }
    public void ForDE22()
    {
        locations[3].gameObject.SetActive(true);
        locations[4].gameObject.SetActive(true);
        locations[5].gameObject.SetActive(true);
        locations[6].gameObject.SetActive(true);

        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 6;
        ActorsMoveSpeed[0] = 1.5f;
        targetLocation[0] = locations[3].position;
        actorIDforChangingLocation = 0;
    }

    public void ForDE25()
    {
        locations[7].gameObject.SetActive(true);
        locations[8].gameObject.SetActive(true);
        locations[9].gameObject.SetActive(true);

        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 3;
        ActorsMoveSpeed[1] = 1;
        targetLocation[1] = locations[8].position;
        actorIDforChangingLocation = 1;
    }
    public void ForDE41()
    {
        ForDE22();
        transition.ManualTransitionON();
        StartCoroutine(ForDE41Coroutine());
    }
    IEnumerator ForDE41Coroutine()
    {
        yield return new WaitForSeconds(5);
        EndingScene();
    }
    public void ForDE50()
    {
        transition.ManualTransitionON();
    }
    public void ForDE53()
    {
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 2;
        StartCoroutine(ForDE53Coroutine());
    }
    IEnumerator ForDE53Coroutine()
    {
        yield return new WaitForSeconds(2);
        EndingScene();

    }
    //END OF ForDE METHODS
    
    public void EndingScene()
    {
        DisableTAKITA = false;
        otherGameObjects[0].SetActive(true);//Enabling for prologue 9 to start
        thisSceneDone = true;
    }

    //Calls from AutoEnterDoor
    public void EnterDoor()
    {
        //dialogueModifier.AddListenersOnConversationEnd();//Remove the Comment to activate this line
        //EndingScene();
    }

    //Calls from LocationChanger
    int actorIDforChangingLocation;//Change this in ForDE methods
    public void ChangeLocation(int actorID, int locationID)
    {
        StartCoroutine(ChangeLocationCoroutine(actorID, locationID));
    }
    IEnumerator ChangeLocationCoroutine(int actorID, int locationID)
    {
        if (actorID == 0 && (locationID == 4 || locationID == 6))
        {
            yield return new WaitForSeconds(0.3f);
        }
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
            DisableTAKITA = this.DisableTAKITA,
            startThisScene = this.startThisScene
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.thisSceneDone = saveData.thisSceneDone;
        this.startThisScene = saveData.startThisScene;
        this.DisableTAKITA = saveData.DisableTAKITA;
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

        public bool DisableTAKITA;
    }
}