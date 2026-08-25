using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    #region Singleton
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Dialogue Manager found!");
            return;
        }
        instance = this;
    }
    #endregion

    public CharacterInfo currentCharacter;
    public int textNum = 0;

    DialogueInteraction currentInteraction;
    SubtitleManager subtitleManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        subtitleManager = SubtitleManager.instance;
        /*if (currentCharacter)
        {
            currentInteraction = currentCharacter.dialogueData.interactions[0];
            subtitleManager.SetText(currentInteraction.dialogue[textNum].dialogueLine);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartInteraction(CharacterInfo[] interactedChar, DialogueInteraction interaction, int newNum)
    {
        currentCharacter = interactedChar[1];
        currentInteraction = interaction;
        textNum = newNum;

        subtitleManager.CallStartDialogue(interactedChar);
    }

    public string GetText()
    {
        return currentInteraction.dialogue[textNum].dialogueLine;
    }

    public string Continue()
    {
        // End dialogue interaction if finished with all queued dialogue
        if (currentInteraction.dialogue.Length - 1 == textNum)
        {
            subtitleManager.FinishDialogue();
            return null;
        }

        // Else, continue cycling through dialogue
        if (currentInteraction.dialogue.Length - 1 > textNum)
        {
            textNum++;
        }

        return currentInteraction.dialogue[textNum].dialogueLine;
    }

    public string Reverse()
    {
        if (textNum - 1 >= 0)
        {
            textNum--;
        }

        return currentInteraction.dialogue[textNum].dialogueLine;
    }

    public void Choice()
    {
        Debug.Log("Show new dialogue options");
    }
}
