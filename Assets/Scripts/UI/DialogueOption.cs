using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour
{
    public TMP_Text textBox;
    public CharDialogueData characterToRespond;
    public int interactionNum = 0;
    public int textNum = 0;
    public Prerequisite[] prerequisite;

    public void InitializeChoice(string answer, CharDialogueData data, int interaction, int text)
    {
        textBox.text = answer;
        characterToRespond = data;
        interactionNum = interaction;
        textNum = text;
    }
    public void InitializeChoice(string answer, CharDialogueData data, int interaction, int text, Prerequisite[] prereq)
    {
        textBox.text = answer;
        characterToRespond = data;
        interactionNum = interaction;
        textNum = text;

        if (prereq != null)
        {
            prerequisite = prereq;

            foreach (Prerequisite pre in prerequisite)
            {
                if (!PlayerStats.instance.HasPrerequisite(pre.prerequisiteName, pre.GetProgress()))
                {
                    LockAnswer();
                    return;
                }
            }
        }
    }

    public void ChooseAnswer()
    {
        if (characterToRespond)
        {
            SubtitleManager.instance.AnswerQuestion();
            DialogueInteraction interaction = characterToRespond.interactions[interactionNum];
            DialogueManager.instance.SwapCharacter(characterToRespond, interaction, textNum);
        }
        else
        {
            Debug.LogWarning("No character to swap to!");
        }
    }

    public void LockAnswer()
    {
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<TMP_Text>().text = "<color=#ffffffff>Locked</color>";
    }
}
