using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    [TextArea]
    public string dialogueLine = "";

    public DialogueNode()
    {
        dialogueLine = new string("");
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
        SubtitleManager.instance.ShowDialogueOptions(dialogueLine, choices);

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
        DialogueInteraction interaction = swapTo.interactions[interactionNum];
        DialogueManager.instance.SwapCharacter(swapTo, interaction, textNum);
        return null;
    }
}

[Serializable]
public class PrerequisiteCheck : DialogueNode
{
    public string[] prerequisites;

    public PrerequisiteCheck() { }

    public override string Process()
    {
        string list = "";
        foreach(string item in prerequisites)
        {
            list += item + ", ";
        }
        return dialogueLine;
    }
}
