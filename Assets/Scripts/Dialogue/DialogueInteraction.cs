using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueInteraction
{
    public string interactionName;
    [SerializeReference]
    public DialogueNode[] dialogue;

    public DialogueInteraction()
    {
        dialogue = new DialogueNode[0];
    }
}
