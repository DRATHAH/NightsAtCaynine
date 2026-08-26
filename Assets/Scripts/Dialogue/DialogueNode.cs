using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

[Serializable]
public class DialogueNode
{
    [TextArea]
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
    public AnswerChoice[] choices;

    public DialogueOptions() { }

    public DialogueOptions(string question, AnswerChoice[] options)
    {
        dialogueLine = question;
        choices = options;
    }

    public override string Process()
    {
        Debug.Log(dialogueLine + " is the question");
        foreach (AnswerChoice choice in choices)
        {
            Debug.Log(choice.dialogueLine);
        }

        return dialogueLine;
    }
}

[Serializable]
public class AnswerChoice : DialogueNode
{
    public CharDialogueData charcterResponding;
    public int interactionNum = 0;
    public int textNum = 0;

    public AnswerChoice() { }

    public AnswerChoice(string answer, CharDialogueData character, int interaction, int text)
    {
        dialogueLine = answer;
        charcterResponding = character;
        interactionNum = interaction;
        textNum = text;
    }

    public override string Process()
    {
        Debug.Log("Answered");
        return null;
    }
}

[Serializable]
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
