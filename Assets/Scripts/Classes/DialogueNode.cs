using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueNode
{
    public List<DialogueNode> children = new List<DialogueNode>();
    public int currentChild = 0;
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
        return children[currentChild].Process();
    }

    public void AddChild(DialogueNode dN)
    {
        children.Add(dN);
    }
}
