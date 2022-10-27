using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class Prologue2 : MonoBehaviour, CutScenes, ISaveable
{
    [Header("Disable object and Scripts")]
    public GameObject inGameUI;
    public GameObject player;

    [Header("Initial Data")]
    public bool thisSceneDone;
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
        if (!thisSceneDone)
        {
            for (int i = 0; i < startMove.Length; i++)
            {
                MoveCharacter(startMove[i], actors[i], anim[i], targetLocation[i], ActorsMoveSpeed[i]);
            }
        }
        else
        {
            StartCoroutine(DisableThisScene());
        }
    }
    IEnumerator DisableThisScene()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    void Disables(bool turn)
    {
        inGameUI.SetActive(turn);
        player.GetComponent<PlayerControls>().enabled = turn;
        player.GetComponent<CharacterAnimation>().ResetAnimation();

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
        
        actors[1].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 4;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Optional;
        Disables(false);
    }
    public void ForDE17()
    {
        StartCoroutine(ForDE17Coroutine());
        targetLocation[2] = locations[4].transform.position;
    }
    IEnumerator ForDE17Coroutine()
    {
        targetLocation[2] = locations[4].transform.position;
        yield return new WaitForSeconds(1f);
        targetLocation[2] = locations[2].transform.position;
        yield return null;
    }
    public void ForDE29()
    {
        transition.ManualTransitionON();
        targetLocation[1] = locations[8].transform.position;
        targetLocation[2] = locations[9].transform.position;
        targetLocation[3] = locations[10].transform.position;
    }
    
    public void ForDE33()
    {
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
    public void ForDE40()
    {
        doors[0].Interact();
    }
    public void ForDE41()
    {
        targetLocation[0] = locations[11].transform.position;
        ActorsMoveSpeed[0] = 0.5f;

        actors[4].SetActive(true);
        startMove[4] = true;
        targetLocation[4] = locations[12].transform.position;
    }
    
    public void ForDE44()
    {
        doors[1].Interact();
    }

    public void StartMove(int id)
    {
        startMove[id] = true;
        if (id == 0)
        {
            targetLocation[id] = locations[0].transform.position;
        }
        if (id == 1)
        {
            actors[id].SetActive(true);
            targetLocation[id] = locations[1].transform.position;
        }
        if (id == 2)
        {
            actors[id].SetActive(true);
            targetLocation[id] = locations[2].transform.position;
        }
        if (id == 3)
        {
            actors[id].SetActive(true);
            targetLocation[id] = locations[3].transform.position;
        }

    }
    public void StartMoving()
    {

    }
    public void EnterDoor()
    {

    }

    public void ChangeLocation(int i)
    {

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
