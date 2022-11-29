using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class Prologue1 : MonoBehaviour, CutScenes, ISaveable
{
    BackgroundMusicScript bgm;
    //important to be saved
    public bool thisSceneDone;
    public bool startThisScene;
    [Header("Disable object and Scripts")]
    public GameObject inGameUI;
    public GameObject player;
    DialogueSystemController dialogueSystemController;
    [Header("Actors")]
    public GameObject[] actor;
    Vector3 movingAnim;
    CharacterAnimation pAnim;
    [HeaderAttribute("Goto Locations")]
    public GameObject[] loc;
    Vector3 target;

    [Header("Data")]
    public float mSpeed;
    public int locNum;
    public bool startMove;
    public PortalDoor door;

    void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<BackgroundMusicScript>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        pAnim = actor[0].GetComponent<CharacterAnimation>();
    }
    void Update()
    {
        if (startThisScene)//true 
        {
            if (!thisSceneDone)//true
            {
                if (startMove)
                {
                    movingAnim = target - actor[0].transform.position;
                    target.y = actor[0].transform.position.y;
                    actor[0].transform.position = Vector3.MoveTowards(actor[0].transform.position, target, mSpeed * Time.deltaTime);
                    pAnim.moveX = movingAnim.x;
                    pAnim.moveZ = movingAnim.z;
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
        for (int i = 0; i < loc.Length; i++)
        {
            loc[i].gameObject.SetActive(false);
        }
    }


    void Disables(bool turn)
    {
        inGameUI.SetActive(turn);
        player.GetComponent<PlayerControls>().enabled = turn;
        player.GetComponent<CharacterAnimation>().ResetAnimation();

    }
    public void ForDE1()
    {
        bgm.ChangeBGM(0);
        startThisScene = true;
        actor[1].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.None;
        Disables(false);
    }
    public void GoOutSide()
    {
        locNum = 0;
        dialogueSystemController.displaySettings.subtitleSettings.continueButton = DisplaySettings.SubtitleSettings.ContinueButtonMode.Never;
        
        target = loc[locNum].transform.position;
        StartMoving();
    }
    
    public void StartMoving()
    {
        startMove = true;
    }
    public void ChangeLocation(int actorID, int locationID)
    {
        target = loc[locationID].transform.position;
    }
    
    public void EnterDoor()
    {
        /*
        startMove = false;
        target = loc[2].transform.position;
        target.y = actor[0].transform.position.y;
        actor[0].transform.position = target;
        */
        dialogueSystemController.displaySettings.subtitleSettings.minSubtitleSeconds = 0.2f;
        EndingScene();
        
    }
    public void EndingScene()
    {
        door.Interact();
        //target = Vector3.zero;
        thisSceneDone = true;
    }
    public void LocationCheck()
    {
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