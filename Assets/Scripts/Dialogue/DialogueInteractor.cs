using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    public CharacterInfo[] speakers;
    public int textNum;
    public string interactionName;

    public void StartInteraction()
    {
        DialogueInteraction interaction = speakers[1].dialogueData.interactions[0];
        DialogueManager.instance.StartInteraction(speakers, interaction, textNum);
    }
}
