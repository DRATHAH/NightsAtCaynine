using UnityEngine;

[System.Serializable]
public class DialogueInteraction
{
    public string interactionName;
    [SerializeReference]
    public DialogueNode[] dialogue;
}
