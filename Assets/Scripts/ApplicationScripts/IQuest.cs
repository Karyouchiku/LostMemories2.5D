using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IQuest
{
    public static event HandledQuest OnNewQuest;
    public delegate void HandledQuest(string quest);
    public static void SetQuest(string quest)
    {
        OnNewQuest?.Invoke(quest);
    }
}
