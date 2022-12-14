using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Prologue4 : MonoBehaviour, CutScenes, ISaveable
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

    BlackTransitioning transition;
    void Start()
    {
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
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].gameObject.SetActive(false);
        }
        actors[1].SetActive(false);
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
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 3;
        for (int i = 0; i < actors.Length; i++)
        {
            startMove[i] = true;
            targetLocation[i] = locations[i].position;

        }
        player.GetComponent<DialogueSystemEvents>().conversationEvents.onConversationEnd.RemoveAllListeners();
    }
    public void ForDE39()
    {
        StartCoroutine(ForDE39Coroutine());
    }
    IEnumerator ForDE39Coroutine()
    {
        yield return new WaitForSeconds(2);
        targetLocation[1] = locations[8].position;
        ActorsMoveSpeed[1] = 2f;
        yield return new WaitForSeconds(2);
        targetLocation[0] = locations[7].position;
        ActorsMoveSpeed[0] = 0.8f;
        yield return new WaitForSeconds(1);
        actors[1].SetActive(false);
    }

    public DialogueModifier dialogueModifier;
    public void EnterDoor()
    {
        doors[0].Interact();
        //dialogueModifier.AddListenersOnConversationEnd();
        EndingScene();
    }
    

    bool tpAvalable = true;
    public void TeleportTolocation()
    {
        if (tpAvalable)
        {
            StartCoroutine(TeleportTolocationCoroutine());
        }
    }
    IEnumerator TeleportTolocationCoroutine()
    {
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1);
        Vector3 loc1 = locations[5].position, loc2 = locations[6].position;
        loc1.y = actors[0].transform.position.y;
        loc2.y = actors[1].transform.position.y;
        actors[0].transform.position = loc1;
        actors[1].transform.position = loc2;
        yield return new WaitForSeconds(0.5f);
        transition.ManualTransitionOFF();
    }
    public void TurnStopLoop()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        stopLooping = false;
    }
    bool stopLooping;

    public void ChangeLocation(int actorID, int locationID)
    {
        if (!stopLooping)
        {
            StartCoroutine(LoopingTransition(locationID));
        }
    }
    IEnumerator LoopingTransition(int i)
    {
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1);
        Vector3 loc1 = locations[i].position, loc2 = locations[i + 1].position;
        loc1.y = actors[0].transform.position.y;
        loc2.y = actors[1].transform.position.y;
        actors[0].transform.position = loc1;
        actors[1].transform.position = loc2;
        transition.ManualTransitionOFF();
    } 

    public void EndingScene()
    {
        thisSceneDone = true;
    }

    public void LocationCheck()
    {
        tpAvalable = false;
    }


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