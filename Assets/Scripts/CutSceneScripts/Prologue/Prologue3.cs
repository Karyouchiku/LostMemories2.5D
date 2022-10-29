using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue3 : MonoBehaviour, CutScenes, ISaveable
{
    [Header("Disable object and Scripts")]
    public GameObject inGameUI;
    public GameObject player;

    //[Header("Initial Data")]
    bool thisSceneDone;
    bool[] startMove;
    DialogueSystemController dialogueSystemController;
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
        startMove = new bool[actors.Length];
        targetLocation = new Vector3[actors.Length];
        anim = new CharacterAnimation[actors.Length];
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        transition = transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        for (int i = 0; i < actors.Length; i++)
        {
            anim[i] = actors[i].GetComponent<CharacterAnimation>();
        }
    }
    bool startThisScene;
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

    public void ForDE1()
    {
        actors[2].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;

        for (int i = 2; i < actors.Length; i++)
        {
            startMove[i] = true;
            targetLocation[i] = locations[i].position;
        }
        
    }
    public void ForDE3()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        StartCoroutine(ForDE3Coroutine());
        transition.ManualTransitionON();
    }
    IEnumerator ForDE3Coroutine()
    {
        int id = 0;
        targetLocation[id] = locations[6].position;
        ActorsMoveSpeed[id] = 2.3f;
        yield return new WaitForSeconds(0.5f);
        id = 2;
        targetLocation[id] = locations[7].position;
        ActorsMoveSpeed[id] = 2.5f;
        id = 3;
        targetLocation[id] = locations[8].position;
        ActorsMoveSpeed[id] = 2.5f;
        id = 4;
        targetLocation[id] = locations[9].position;
        ActorsMoveSpeed[id] = 2.5f;
    }
    public void ForDE4()
    {
        transition.ManualTransitionON();
    }
    public void ForDE12()
    {
        transition.ManualTransitionOFF();
    }
    public void ForDE13()
    {
        transition.ManualTransitionOFF();
        int id = 1;
        actors[id].SetActive(true);
        startMove[id] = true;
        targetLocation[id] = locations[5].position;
        ActorsMoveSpeed[id] = 2.5f;
    }
    public void ForDE14()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        transition.ManualTransitionON();
    }

    public void ForDE18()
    {
        for (int i = 2; i < actors.Length; i++)
        {
            targetLocation[i] = locations[10].position;
            ActorsMoveSpeed[i] = 2.3f;
        }
        StartCoroutine(ForDE18Coroutine());
    }
    IEnumerator ForDE18Coroutine()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 2; i < actors.Length; i++)
        {
            actors[i].gameObject.SetActive(false);
        }
    }
    public void ForDE20()
    {
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        targetLocation[1] = locations[12].position;
        ActorsMoveSpeed[1] = 1f;
    }
    public void ForDE22()
    {
        targetLocation[1] = locations[13].position;
        ActorsMoveSpeed[1] = 1f;
    }

    public void ForDE23()
    {
        targetLocation[1] = locations[11].position;
        ActorsMoveSpeed[1] = 0.7f;
    }
    public void ForDE31()
    {
        int id = 2;
        targetLocation[id] = locations[14].position;
        ActorsMoveSpeed[id] = 0.7f;
        id = 3;
        targetLocation[id] = locations[15].position;
        ActorsMoveSpeed[id] = 0.7f;
        id = 4;
        targetLocation[id] = locations[16].position;
        ActorsMoveSpeed[id] = 0.7f;
        StartCoroutine(ForDE31Coroutine());
    }
    IEnumerator ForDE31Coroutine()
    {
        yield return new WaitForSeconds(4f);
    }
    public void ForDE33()
    {
        actors[1].gameObject.SetActive(true);
        startMove[1] = true;
        actors[1].transform.position = locations[17].position;
        targetLocation[1] = locations[18].position;

    }
    public void ForDE43()
    {
        doors[0].Interact();
        EndingScene();
    }

    public void StartMoving()
    {
        startThisScene = true;
        startMove[0] = true;
        targetLocation[0] = locations[0].position;
    }

    public void EnterDoor()
    {

    }

    public void ChangeLocation(int i)
    {
        targetLocation[0] = locations[i].position;
    }

    public void EndingScene()
    {
        thisSceneDone = true;
    }


    public object SaveState()
    {
        return new SaveData()
        {
            thisSceneDone = this.thisSceneDone,
            startMove = this.startMove
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.thisSceneDone = saveData.thisSceneDone;
        this.startMove = saveData.startMove;
    }

    public void LocationCheck()
    {
        throw new NotImplementedException();
    }

    [Serializable]
    struct SaveData
    {
        public bool thisSceneDone;
        public bool[] startMove;
    }
}
