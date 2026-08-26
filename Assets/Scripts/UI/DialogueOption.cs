using TMPro;
using UnityEngine;

public class DialogueOption : MonoBehaviour
{
    public TMP_Text textBox;
    public CharDialogueData characterToRespond;
    public int interactionNum = 0;
    public int textNum = 0;

    public void InitializeChoice(string answer, CharDialogueData data, int interaction, int text)
    {
        textBox.text = answer;
        characterToRespond = data;
        interactionNum = interaction;
        textNum = text;
    }

    public void ChooseAnswer()
    {
        if (characterToRespond)
        {
            DialogueInteraction interaction = characterToRespond.interactions[interactionNum];
            DialogueManager.instance.SwapCharacter(characterToRespond, interaction, textNum);
            SubtitleManager.instance.AnswerQuestion();
        }
        else
        {
            Debug.LogWarning("No character to swap to!");
        }
    }
}
