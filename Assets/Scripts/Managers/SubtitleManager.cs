using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    #region Singleton

    public static SubtitleManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SubtitleManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    [Header("Display")]
    [SerializeField] Animator dialogueAnimation;
    [SerializeField] Subtitle dialogueBox;
    [SerializeField] TMP_Text messageText;
    [SerializeField] CharIconData[] characterIcons;
    [SerializeField] QuestionUI dialogueOptions;

    [Tooltip("Speed at which each character is typed in at")]
    public float time = 0.02f;

    [Header("Other")]
    public InputActionReference mouseClick;
    public InputActionReference rightMouseClick;

    DialogueManager dialogueManager;
    bool canClick = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = DialogueManager.instance;

        dialogueAnimation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mouseClick.action.triggered && dialogueAnimation.gameObject.GetComponent<CanvasGroup>().alpha == 1 && dialogueAnimation.gameObject.activeSelf && canClick)
        {
            if (!dialogueBox.finished)
            {
                FinishText(dialogueManager.GetText());
            }
            else
            {
                SetText(dialogueManager.Continue());
            }
        }

        if (rightMouseClick.action.triggered)
        {
            SetText(dialogueManager.Reverse());
        }
    }

    public void SetContinueState(bool state)
    {
        canClick = state;
    }

    public void CallStartDialogue(CharDialogueData[] characters)
    {
        StartCoroutine(StartDialogue(characters));
    }

    public IEnumerator StartDialogue(CharDialogueData[] characters)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characterIcons[i].AssignCharacter(characters[i]);
            Debug.Log(characterIcons[i].name);
        }

        dialogueAnimation.gameObject.SetActive(true);
        dialogueBox.ClearDialogue(messageText);
        if (!dialogueAnimation.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            dialogueAnimation.SetTrigger("Activate");
        }

        foreach (CharIconData character in characterIcons)
        {
            character.UpdateVisual(CharIconData.Emotion.Resting, dialogueManager.currentCharacter.name);
        }

        while (!dialogueAnimation.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            yield return null;
        }

        while (dialogueAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 )
        {
            yield return null;
        }

        SetText(dialogueManager.GetText());
    }

    // Normally adds text to the dialogue box
    public void SetText(string msg)
    {
        if (msg != null)
        {
            messageText.color = DialogueManager.instance.currentCharacter.dialogueColor;
            dialogueBox.AddWriter(messageText, msg, time, true);
        }

        foreach (CharIconData character in characterIcons)
        {
            character.UpdateVisual(CharIconData.Emotion.Resting, dialogueManager.currentCharacter.name);
        }
    }

    // Immediately sets the dialogue box to the text
    public void FinishText(string msg)
    {
        dialogueBox.SetDialogue(messageText, msg);
    }

    public void FinishDialogue()
    {
        dialogueBox.ClearDialogue(messageText);
        dialogueAnimation.SetTrigger("Deactivate");
    }

    public void ShowDialogueOptions(string question, AnswerChoice[] answers)
    {
        dialogueAnimation.SetBool("QuestionAnswer",true);
        dialogueOptions.gameObject.SetActive(true);
        dialogueOptions.questionText.text = question;
        foreach (AnswerChoice ans in answers)
        {
            dialogueOptions.AddAnswer(ans);
        }
    }

    public void AnswerQuestion()
    {
        dialogueOptions.ClearAnswers();
        dialogueAnimation.SetBool("QuestionAnswer", true);
        dialogueAnimation.SetBool("Activate", true);
        dialogueAnimation.SetBool("QuestionAnswer", false);
    }
}
