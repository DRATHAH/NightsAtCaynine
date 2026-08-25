using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

[System.Serializable]
public class DialogueNode
{
    public string dialogueLine = "";

    public DialogueNode()
    {

    }

    public DialogueNode(string text)
    {
        dialogueLine = text;
    }

    public virtual string Process()
    {
        return dialogueLine;
    }
}

[Serializable]
public class DialogueSequence : DialogueNode
{
    [SerializeReference]
    public List<DialogueNode> nodes = new List<DialogueNode>();

    public DialogueSequence() { }

    public DialogueSequence(string text)
    {
        dialogueLine = text;
    }

    public override string Process()
    {
        foreach(DialogueNode node in nodes)
        {
            return node.Process();
        }

        return null;
    }
}

[Serializable]
public class DialogueOptions : DialogueNode
{
    public string[] choices;

    public DialogueOptions() { }

    public DialogueOptions(string question, string[] options)
    {
        dialogueLine = question;
        choices = options;
    }

    public override string Process()
    {
        Debug.Log(dialogueLine + " is the question");
        foreach (string choice in choices)
        {
            Debug.Log(choice);
        }

        return dialogueLine;
    }
}

public class DialogueSwapSpeaker : DialogueNode
{
    public DialogueSwapSpeaker() { }

    [SerializeField]
    public CharDialogueData swapTo;
    public int interactionNum;
    public int textNum = 0;

    public override string Process()
    {
        Debug.Log("Swapped");
        DialogueInteraction interaction = swapTo.interactions[interactionNum];
        DialogueManager.instance.SwapCharacter(swapTo, interaction, textNum);
        return null;
    }
}
