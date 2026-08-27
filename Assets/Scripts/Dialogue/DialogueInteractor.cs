using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    public CharDialogueData[] speakers;
    public int textNum;
    public int interactionNum;

    public List<DialogueSwapSpeaker> repeatedDialogues;

    int repeated = 0;

    public void StartInteraction()
    {
        DialogueInteraction interaction = speakers[1].interactions[interactionNum];
        DialogueManager.instance.StartInteraction(speakers, interaction, textNum);

        repeated++;
        repeated = Mathf.Clamp(repeated, 0, repeatedDialogues.Count);

        if (repeatedDialogues.Count > 0)
        {
            textNum = repeatedDialogues[repeated - 1].textNum;
            interactionNum = repeatedDialogues[repeated - 1].interactionNum;
        }
    }
}
