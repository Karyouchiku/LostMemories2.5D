using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestAndObjectives : MonoBehaviour, ISaveable
{
    public TMP_Text quest;
    public TMP_Text objectives1;
    public TMP_Text objectives2;

    void OnEnable()
    {
        IQuest.OnNewQuest += SetQuest;
        IObjectives.OnNewObjective1 += SetObjective1;
        IObjectives.OnNewObjective2 += SetObjective2;
        
        IObjectives.OnHighlightObjective1 += HighlightObjective1;
    }
    void OnDisable()
    {
        IQuest.OnNewQuest -= SetQuest;
        IObjectives.OnNewObjective1 -= SetObjective1;
        IObjectives.OnNewObjective2 -= SetObjective2;
    }
    void SetQuest(string quest)
    {
        this.quest.text = quest;
    }
    void HighlightObjective1()
    {
        objectives1.GetComponent<Animator>().ResetTrigger("Highlight");
        objectives1.GetComponent<Animator>().SetTrigger("Highlight");
    }
    void SetObjective1(string objectives)
    {
        this.objectives1.text = objectives;
    }
    void SetObjective2(string objectives)
    {
        this.objectives2.text = objectives;
    }
    public object SaveState()
    {
        return new SaveData()
        {
            quest = this.quest.text,
            objectives1 = this.objectives1.text,
            objectives2 = this.objectives2.text
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        StartCoroutine(SetCurrentQuest(saveData));
    }
    IEnumerator SetCurrentQuest(SaveData saveData)
    {
        yield return new WaitForFixedUpdate();
        this.quest.text = saveData.quest;
        this.objectives1.text = saveData.objectives1;
        this.objectives2.text = saveData.objectives2;
    }

    [Serializable]
    struct SaveData
    {
        public string quest;
        public string objectives1;
        public string objectives2;
    }
}
