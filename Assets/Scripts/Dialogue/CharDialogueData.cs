using UnityEngine;

[CreateAssetMenu(fileName = "New Character Dialogue Data", menuName = "Dialogue/Character Dialogue Data")]
public class CharDialogueData : ScriptableObject
{
    public DialogueInteraction[] interactions;
}
