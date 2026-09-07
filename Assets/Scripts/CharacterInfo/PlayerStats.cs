using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    #region Singleton

    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Player Stats found!");
            return;
        }

        instance = this;
    }

    #endregion

    public InputActionReference cheatKey;
    public Prerequisite cheatPrereq;

    // For prerequisites that are 'achieved' and not 'amount', 1 = true, 0 = false
    public Dictionary<string, Prerequisite> prerequisiteProgress = new Dictionary<string, Prerequisite>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cheatKey.action.triggered)
        {
            AddPrerequisite(cheatPrereq.prerequisiteName, cheatPrereq.type);
            Debug.Log("added prereq");
        }
    }

    public void AddPrerequisite(string name, Prerequisite.PrerequisiteType type)
    {
        if (!prerequisiteProgress.ContainsKey(name))
        {
            prerequisiteProgress.Add(name, new Prerequisite(name, type));
        }
        else
        {
            prerequisiteProgress[name].AddProgress();
        }
    }

    public bool HasPrerequisite(string name, int amount)
    {
        if (prerequisiteProgress.ContainsKey(name) && prerequisiteProgress[name].GetProgress() >= amount)
        {
            return true;
        }

        return false;
    }
}
