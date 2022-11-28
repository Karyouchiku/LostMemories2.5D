using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using TMPro;

public class PuzzleCh2 : MonoBehaviour, IPuzzle2, ISaveable
{
    #region Intance Variables
    [Header("Important for Saving Progress")]
    public bool startThisPuzzle;
    public bool thisPuzzleDone;

    [Header("Player Data")]
    public GameObject player;
    public GameObject inGameUI;
    public CamFollowPlayer cam;

    [Header("Enemy FOV")]
    public FieldOfView ped;
    public FieldOfView ar;
    public FieldOfView shady;

    [Header("Dialogue Database")]
    public DialogueDatabase ddb;
    DialogueSystemTrigger triggerDialogue;
    DialogueSystemController dialogueSystemController;
    [Header("Item Involved")]
    public Item item;
    public LockInteractableDoors door;

    bool[] startMove;
    GameObject[] actors;
    LMActors lmActors;
    float[] ActorsMoveSpeed;
    CharacterAnimation[] anim;
    BlackTransitioning transition;
    DialogueModifier dialogueModifier;
    WorldActiveSaveState worldRenderer;
    HideFloors firstFloor;
    HideFloors secondFloor;


    [Header("Use for Locations: Moving object to these locations")]
    public GameObject[] GameObjectChildrens;
    Vector3[] targetLocation;
    Vector3 animVec;
    #endregion
    #region Start Method -- Defining instance variables
    void Start()
    {
        firstFloor = GameObject.Find("WHI First Floor").GetComponent<HideFloors>();
        secondFloor = GameObject.Find("WHI Second Floor").GetComponent<HideFloors>();

        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
        worldRenderer = GetComponentInParent<WorldActiveSaveState>();

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
    #endregion
    #region Puzzle Methods
    /// <summary>
    /// Present ActorID Lists
    /// 15, 18, 19
    /// 
    /// 15 = nasa living room
    /// 18 = nasa 2nd floor hallway
    /// 19 = 1st floor hallway and rooms
    /// </summary>
    void Update()
    {
        if (startThisPuzzle)
        {
            if (!thisPuzzleDone)
            {
                for (int i = 0; i < startMove.Length; i++)
                {
                    MoveCharacter(startMove[i], actors[i], anim[i], targetLocation[i], ActorsMoveSpeed[i]);
                }
            
                if (ar.canSeePlayer || ped.canSeePlayer || shady.canSeePlayer)
                {
                    if (ar.canSeePlayer)
                    {
                        OnPlayerCaught(19, 50);
                        Debug.Log("Ar saw you");
                    }
                    if (ped.canSeePlayer)
                    {
                        OnPlayerCaught(18, 49);
                        Debug.Log("Ped saw you");
                    }
                    EnemyFOVs(false);
                }
            }
            
        }
    }
    #endregion
    #region Shortcuts
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
        else
        {
            pAnim.moveX = 0;
            pAnim.moveZ = 0;
        }
    }
    public void StartThisPuzzle()
    {
        startThisPuzzle = true;
        HideFloors.enableDisablingActors = true;

        actors[18].tag = "NPC";
        actors[19].tag = "NPC";
        DisableChilds();
        StartEnemyRoutine();
        EnemyFOVs(true);
    }

    void DisableChilds()
    {
        foreach (GameObject child in GameObjectChildrens)
        {
            child.SetActive(false);
        }
    }
    void StartEnemyRoutine()
    {
        SetActorStartingPosition(15, 16);
        SetActorStartingPosition(18, 0);
        SetActorStartingPosition(19, 9);
        GotoPosition(18, 1, 0.5f);
        GotoPosition(19, 10, 0.5f);
    }
    public void StopCharacter(int actorID)
    {
        startMove[actorID] = false;
    }

    public void EndingPuzzle()
    {
        thisPuzzleDone = true;
        ResetActorPositionToOriginal(15);
        ResetActorPositionToOriginal(18);
        ResetActorPositionToOriginal(19);
        DisableChilds();
        EnemyFOVs(false);
    }
    public void OnPlayerCaught(int actorID, int convoID)
    {
        StopCharacter(actorID);
        triggerDialogue = actors[actorID].GetComponent<DialogueSystemTrigger>();
        triggerDialogue.trigger = DialogueSystemTriggerEvent.OnUse;
        triggerDialogue.conversation = ddb.GetConversation(convoID).Title;
        triggerDialogue.OnUse();
        triggerDialogue.trigger = DialogueSystemTriggerEvent.None;

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
    int saveLocation18, saveLocation19;
    float saveMoveSpeed18, saveMoveSpeed19;
    public void GotoPosition(int actorID, int locationID)// 1
    {
        if (actorID == 18)
        {
            saveLocation18 = locationID;
        }
        if (actorID == 19)
        {
            saveLocation19 = locationID;
        }

        startMove[actorID] = true;
        actors[actorID].SetActive(true);
        GameObjectChildrens[locationID].SetActive(true);
        targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
    }
    public void GotoPosition(int actorID, int locationID, float moveSpeed)// 2
    {
        if (actorID == 18)
        {
            saveLocation18 = locationID;
            saveMoveSpeed18 = moveSpeed;
        }
        if (actorID == 19)
        {
            saveLocation19 = locationID;
            saveMoveSpeed19 = moveSpeed;
        }
        startMove[actorID] = true;
        actors[actorID].SetActive(true);
        GameObjectChildrens[locationID].SetActive(true);
        ActorsMoveSpeed[actorID] = moveSpeed;
        targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
    }
    public void GotoPosition(int actorID, int locationID, float moveSpeed, float waitSec)// 3
    {
        if (actorID == 18)
        {
            saveLocation18 = locationID;
        }
        if (actorID == 19)
        {
            saveLocation19 = locationID;
        }
        startMove[actorID] = true;
        actors[actorID].SetActive(true);
        GameObjectChildrens[locationID].SetActive(true);
        ActorsMoveSpeed[actorID] = moveSpeed;
        StartCoroutine(GotoPositionRoutine(actorID, locationID, waitSec));
    }
    IEnumerator GotoPositionRoutine(int actorID, int locationID, float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        targetLocation[actorID] = GameObjectChildrens[locationID].transform.position;
    }
    void EnemyFOVs(bool onEnabled)
    {
        if (!onEnabled)
        {
            ar.canSeePlayer = onEnabled;
            ped.canSeePlayer = onEnabled;
            shady.canSeePlayer = onEnabled;
        }

        ar.enabled = onEnabled;
        ped.enabled = onEnabled;
        shady.enabled = onEnabled;
    }
    public void ForDEStart()
    {
        ContinueMode(true);
    }
    public void ForDENarration()
    {
        ContinueMode(false);
        transition.ManualTransitionON();
    }
    public void ForDETryAgain()
    {
        StartCoroutine(RestartingPuzzle());
    }

    IEnumerator RestartingPuzzle()
    {
        item.isActive = true;
        door.isUnlocked = false;
        door.gameObject.tag = "InteractableObject";
        worldRenderer.RenderWorlds(false, 11);
        SetActorStartingPosition(0, 8);

        firstFloor.ManualEnableActorSprites(false);
        secondFloor.ManualEnableActorSprites(true);

        StartEnemyRoutine(); 
        EnemyFOVs(true);
        yield return new WaitForSeconds(1f);
        transition.ManualTransitionOFF();

        //player.GetComponent<PlayerControls>().enabled = true;
        //inGameUI.SetActive(true);
    }

    void SetActorStartingPosition(int actorID, int locationID)
    {
        if (actorID == 0)
        {
            player.GetComponent<CharacterController>().enabled = false;
        }
        actors[actorID].SetActive(true);
        actors[actorID].transform.position = GameObjectChildrens[locationID].transform.position;
        if (actorID == 0)
        {
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
    void ResetActorPositionToOriginal(int actorID)
    {
        actors[actorID].tag = "Untagged";
        actors[actorID].transform.position = lmActors.orginalActorLocations[actorID];
        targetLocation[actorID] = lmActors.orginalActorLocations[actorID];
        actors[actorID].GetComponent<CharacterAnimation>().ResetAnimation();
        actors[actorID].SetActive(false);
    }
    #endregion
    #region Save System
    public object SaveState()
    {
        int[,] locationCollections = new int[2, 2];
        float[,] moveSpeedCollections = new float[2, 2];
        
        locationCollections[0, 0] = 18;
        locationCollections[0, 1] = saveLocation18;


        locationCollections[1, 0] = 19;
        locationCollections[1, 1] = saveLocation19;
        
        moveSpeedCollections[0, 0] = 18;
        moveSpeedCollections[0, 1] = saveMoveSpeed18;
        
        moveSpeedCollections[1, 0] = 19;
        moveSpeedCollections[1, 1] = saveMoveSpeed19;

        return new SaveData()
        {
            thisPuzzleDone = this.thisPuzzleDone,
            startThisPuzzle = this.startThisPuzzle,
            actorLocationIDs = locationCollections,
            actormoveSpeed = moveSpeedCollections
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        thisPuzzleDone = saveData.thisPuzzleDone;
        startThisPuzzle = saveData.startThisPuzzle;
        if (!thisPuzzleDone && startThisPuzzle)
        {
            //GotoPosition(18, saveData.actorLocationIDs[0,1], saveData.actormoveSpeed[0,1]);
            //GotoPosition(19, saveData.actorLocationIDs[1,1], saveData.actormoveSpeed[1,1]);
        }
    }

    [Serializable]
    struct SaveData
    {
        public bool thisPuzzleDone;
        public bool startThisPuzzle;

        public int[,] actorLocationIDs;
        public float[,] actormoveSpeed;

    }
    #endregion
}