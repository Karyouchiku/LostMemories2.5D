using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueData : ScriptableObject
{
    public string npcName;
    public List<string> stringDialogueParts;
    public List<ScriptableObject> choiceList;
    
}
