using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    public CharDialogueData[] speakers;
    public int textNum;
    public string interactionName;

    public void StartInteraction()
    {
        DialogueInteraction interaction = speakers[1].interactions[0];
        DialogueManager.instance.StartInteraction(speakers, interaction, textNum);
    }
}
