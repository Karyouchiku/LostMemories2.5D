using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestAndObjectives : MonoBehaviour, ISaveable
{
    public TMP_Text quest;
    public TMP_Text objectives;

    void OnEnable()
    {
        IQuest.OnNewQuest += SetQuest;
        IObjectives.OnNewObjectives += SetObjectives;
    }
    void OnDisable()
    {
        IQuest.OnNewQuest -= SetQuest;
        IObjectives.OnNewObjectives -= SetObjectives;
    }
    void SetQuest(string quest)
    {
        this.quest.text = quest;
    }
    void SetObjectives(string objectives)
    {
        this.objectives.text = objectives;
    }

    public object SaveState()
    {
        //Debug.Log($"Current QuestText to Save: {questText.text}");
        return new SaveData()
        {
            quest = this.quest.text
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        //Debug.Log($"Current QuestText to Save: {saveData.questText}");
        StartCoroutine(SetCurrentQuest(saveData.quest));
    }
    IEnumerator SetCurrentQuest(string quest)
    {
        yield return new WaitForFixedUpdate();
        this.quest.text = quest;
    }

    [Serializable]
    struct SaveData
    {
        public string quest;
    }
}
