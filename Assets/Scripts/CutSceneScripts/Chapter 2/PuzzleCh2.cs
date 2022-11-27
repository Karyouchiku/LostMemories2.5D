using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleCh2 : MonoBehaviour, IPuzzle2, ISaveable
{
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

    void Start()
    {
        firstFloor = GameObject.Find("WHI First Floor").GetComponent<HideFloors>();
        secondFloor = GameObject.Find("WHI Second Floor").GetComponent<HideFloors>();

        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
        dialogueModifier = GameObject.Find("Player&Camera").GetComponent<DialogueModifier>();
        transition = GameObject.FindGameObjectWithTag("Canvas").GetComponent<BlackTransitioning>();
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
    /// <summary>
    /// Present ActorID Lists
    /// 15, 18, 19
    /// 
    /// 15 = nasa living room
    /// 18 = nasa 2nd floor hallway
    /// 19 = 1st floor hallway and rooms
    /// </summary>


    public void StartThisPuzzle()
    {
        startThisPuzzle = true;
        HideFloors.enableDisablingActors = true;
        RestartPuzzle(false);
    }
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
                    ar.enabled = false;
                    ped.enabled = false;
                    shady.enabled = false;

                    ar.canSeePlayer = false;
                    ped.canSeePlayer = false;
                    shady.canSeePlayer = false;
                    Debug.Log("Someone saw you");
                    RestartPuzzle(true);
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
        for (int i = 0; i < GameObjectChildrens.Length; i++)
        {
            GameObjectChildrens[i].SetActive(false);
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
        else
        {
            pAnim.moveX = 0;
            pAnim.moveZ = 0;
        }
    }

    public void StopCharacter(int actorID)
    {
        startMove[actorID] = false;
    }
    //IPuzzle Methods

    public void EndingPuzzle()
    {
        thisPuzzleDone = true;
        ResetActorPositionToOriginal(15);
        ResetActorPositionToOriginal(18);
        ResetActorPositionToOriginal(19);
    }

    #region Shortcuts
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
    void RestartPuzzle(bool onRestart)
    {
        actors[18].tag = "NPC";
        actors[19].tag = "NPC";
        foreach (GameObject child in GameObjectChildrens)
        {
            child.SetActive(false);
        }

        if (onRestart)
        {
            //stopping the player
            player.GetComponent<PlayerControls>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<CharacterAnimation>().ResetAnimation();
            inGameUI.GetComponentInChildren<VariableJoystick>().ResetValue();
            inGameUI.SetActive(false);
            
            //stopping the actor
            StopCharacter(18);
            StopCharacter(19);

            StartCoroutine(RestartPuzzleCoroutine());
        }
        else
        {
            SetActorStartingPosition(15, 16);
            SetActorStartingPosition(18, 0);
            SetActorStartingPosition(19, 9);
            GotoPosition(18, 1, 0.5f);
            GotoPosition(19, 10, 0.5f);

            ar.enabled = true;
            ped.enabled = true;
            shady.enabled = true;
        }
    }
    IEnumerator RestartPuzzleCoroutine()
    {
        yield return new WaitForSeconds(1f);
        transition.ManualTransitionON();
        yield return new WaitForSeconds(1f);
        worldRenderer.RenderWorlds(false, 11);
        cam.removeAnimation = true;
        SetActorStartingPosition(0, 8);
        cam.removeAnimation = false;

        firstFloor.ManualEnableActorSprites(false);
        secondFloor.ManualEnableActorSprites(true);

        SetActorStartingPosition(15, 16);
        SetActorStartingPosition(18, 0);
        SetActorStartingPosition(19, 9);
        
        GotoPosition(18, 1, 0.5f);
        GotoPosition(19, 10, 0.5f);

        ar.enabled = true;
        ped.enabled = true;
        shady.enabled = true;

        yield return new WaitForSeconds(1f);
        transition.ManualTransitionOFF();
        player.GetComponent<PlayerControls>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        inGameUI.SetActive(true);
    }

    void SetActorStartingPosition(int actorID, int locationID)
    {
        actors[actorID].SetActive(true);
        actors[actorID].transform.position = GameObjectChildrens[locationID].transform.position;
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