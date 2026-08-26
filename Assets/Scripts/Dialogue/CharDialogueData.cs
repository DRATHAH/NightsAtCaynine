using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Character Dialogue Data", menuName = "Dialogue/Character Dialogue Data")]
public class CharDialogueData : ScriptableObject
{
    [Tooltip("Name of the character speaking")]
    public string characterName = "";
    [Tooltip("Color of the text the character will speak in")]
    public Color dialogueColor = Color.white;
    public DialogueInteraction[] interactions;
}
