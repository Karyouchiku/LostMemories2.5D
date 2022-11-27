using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class Prologue2 : MonoBehaviour, CutScenes, ISaveable
{
    public bool thisSceneDone;
    public bool startThisScene;

    [Header("Disable object and Scripts")]
    public GameObject inGameUI;
    public GameObject player;

    [Header("Initial Data")]
    bool[] startMove;
    public PortalDoor[] doors;
    DialogueSystemController dialogueSystemController;
    [Header("Actors")]
    public GameObject[] actors;
    public float[] ActorsMoveSpeed;
    CharacterAnimation[] anim;
    [Header("Trigger Locations")]
    Vector3[] targetLocation;
    public GameObject[] locations;
    Vector3 animVec;

    BlackTransitioning transition;
    void Start()
    {
        startMove = new bool[actors.Length];
        anim = new CharacterAnimation[actors.Length];
        targetLocation = new Vector3[actors.Length];
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
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
    }


    public void MoveCharacter(bool startMove, GameObject actor, CharacterAnimation pAnim, Vector3 target, float mSpeed)
    {
        if (startMove)
        {
            target.y = actor.transform.position.y;
            animVec = target - actor.transform.position;
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, target, mSpeed * Time.deltaTime);
            pAnim.moveX = animVec.x;
            pAnim.moveZ = animVec.z;
        }
    }
    public void ForDE1()
    {
        startThisScene = true;
        actors[1].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 3;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        startMove[0] = true;
        targetLocation[0] = locations[0].transform.position;
    }
    public void ForDE2()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        
        for (int i = 1; i < (actors.Length-1); i++)
        {
            startMove[i] = true;
            targetLocation[i] = locations[i].transform.position;
        }
    }
    public void ForDE3_8_13()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
    }
    public void ForDE17()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 1.5f;
        targetLocation[2] = locations[4].transform.position;
        ActorsMoveSpeed[2] = 1.3f;
        StartCoroutine(ForDE17Coroutine());
    }
    IEnumerator ForDE17Coroutine()
    {
        yield return new WaitForSeconds(0.3f);
        targetLocation[2] = locations[2].transform.position;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 4;
    }
    public void ForDE29()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        transition.ManualTransitionON();
        targetLocation[1] = locations[8].transform.position;
        targetLocation[2] = locations[9].transform.position;
        targetLocation[3] = locations[10].transform.position;
    }
    
    public void ForDE33()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 2f;
        StartCoroutine(ForDE33Coroutine());
    }
    IEnumerator ForDE33Coroutine()
    {
        targetLocation[1] = locations[5].transform.position;
        targetLocation[2] = locations[6].transform.position;
        targetLocation[3] = locations[7].transform.position;
        ActorsMoveSpeed[1] = 2f;
        ActorsMoveSpeed[2] = 2f;
        ActorsMoveSpeed[3] = 2f;
        yield return new WaitForSeconds(3f);
        for (int i = 1; i < (actors.Length - 1); i++)
        {
            actors[i].SetActive(false);
        }
        yield return null;
    }
    public void ForDE36()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        StartCoroutine(ForDE36Coroutine());
    }
    IEnumerator ForDE36Coroutine()
    {
        targetLocation[1] = locations[5].transform.position;
        targetLocation[2] = locations[6].transform.position;
        targetLocation[3] = locations[7].transform.position;
        yield return new WaitForSeconds(3f);
        for (int i = 1; i < (actors.Length - 1); i++)
        {
            actors[i].SetActive(false);
        }
        yield return null;
    }
    public void ForDE38()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
    }
    public void ForDE40()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        doors[0].Interact();
        EndingScene();
    }
    public void ForDE41()
    {
        targetLocation[0] = locations[11].transform.position;
        ActorsMoveSpeed[0] = 0.5f;

        actors[4].SetActive(true);
        startMove[4] = true;
        targetLocation[4] = locations[12].transform.position;
    }
    public void ForDE43()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 2;
        doors[1].Interact();
        EndingScene();
    }
    public void ForDE44()
    {
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 2;
        doors[1].Interact();
        EndingScene();
    }
    public void StartMoving()
    {

    }
    public void EnterDoor()
    {

    }

    public void ChangeLocation(int actorID, int locationID)
    {
        throw new NotImplementedException();
    }


    
    public void EndingScene()
    {
        StartCoroutine(EndingSceneCoroutine());
    }

    IEnumerator EndingSceneCoroutine()
    {
        yield return new WaitForSeconds(1);
        thisSceneDone = true;
    }

    public void LocationCheck()
    {
        throw new NotImplementedException();
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