using UnityEngine;
using UnityEngine.Events;

public class DialogueLeaf : DialogueNode
{
    public UnityAction afterRunFunction;

    public DialogueLeaf(string text, UnityAction action)
    {
        dialogueLine = text;
        afterRunFunction = action;
    }

    public override string Process()
    {
        if (afterRunFunction != null)
        {
            afterRunFunction();
            Debug.Log(dialogueLine);
            return dialogueLine;
        }
        return null;
    }
}
