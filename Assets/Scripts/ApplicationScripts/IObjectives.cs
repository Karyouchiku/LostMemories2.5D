using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IObjectives
{
    public static event HandledObjective1 OnNewObjective1;
    public delegate void HandledObjective1(string objective);
    public static event HandledObjective2 OnNewObjective2;
    public delegate void HandledObjective2(string objective);

    public static event HandledHighlightObjective1 OnHighlightObjective1;
    public delegate void HandledHighlightObjective1();


    public static void HighlightObjective1()
    {
        OnHighlightObjective1?.Invoke();
    }
    public static void SetObjective1()
    {
        OnNewObjective1?.Invoke("");
    }
    public static void SetObjective1(string objective)
    {
        OnNewObjective1?.Invoke(objective);
    }

    public static void SetObjective2()
    {
        OnNewObjective2?.Invoke("");
    }
    public static void SetObjective2(string objective)
    {
        OnNewObjective2?.Invoke(objective);
    }
}
