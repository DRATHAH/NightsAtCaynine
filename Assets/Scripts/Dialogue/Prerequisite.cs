using System;
using UnityEngine;

[Serializable]
public class Prerequisite
{
    public string prerequisiteName = "";
    public enum PrerequisiteType
    {
        completed,
        amount,
    }
    public PrerequisiteType type = PrerequisiteType.completed;
    public int progress = 1;

    public Prerequisite(string name)
    {
        prerequisiteName = name;
        type = PrerequisiteType.completed;
        progress = 1;
    }

    public Prerequisite(string name, PrerequisiteType prereqType)
    {
        prerequisiteName = name;
        type = prereqType;
        progress = 1;
    }

    public void AddProgress()
    {
        progress++;
    }

    public int GetProgress()
    {
        return progress;
    }
}
