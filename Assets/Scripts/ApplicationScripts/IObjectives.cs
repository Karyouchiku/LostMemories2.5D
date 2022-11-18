using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IObjectives
{
    public static event HandledObjectives OnNewObjectives;
    public delegate void HandledObjectives(string objective);
    public static void SetObjectives(string objective)
    {
        OnNewObjectives?.Invoke(objective);
    }
}
