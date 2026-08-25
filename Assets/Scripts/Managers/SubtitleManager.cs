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

    [Header("Other")]
    public InputActionReference mouseClick;
    public InputActionReference rightMouseClick;

    DialogueManager dialogueManager;

    [Tooltip("Speed at which each character is typed in at")]
    public float time = 0.02f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = DialogueManager.instance;

        dialogueAnimation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mouseClick.action.triggered && dialogueAnimation.gameObject.GetComponent<CanvasGroup>().alpha == 1 && dialogueAnimation.gameObject.activeSelf)
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

    public void CallStartDialogue(CharacterInfo[] characters)
    {
        StartCoroutine(StartDialogue(characters));
    }

    public IEnumerator StartDialogue(CharacterInfo[] characters)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characterIcons[i].AssignCharacter(characters[i]);
            Debug.Log(characterIcons[i].name);
        }

        dialogueAnimation.gameObject.SetActive(true);
        if (!dialogueAnimation.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            dialogueAnimation.SetTrigger("Activate");
        }

        foreach (CharIconData character in characterIcons)
        {
            character.UpdateVisual(CharIconData.Emotion.Resting, dialogueManager.currentCharacter.name);
        }

        while (dialogueAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
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
    }

    // Immediately sets the dialogue box to the text
    public void FinishText(string msg)
    {
        dialogueBox.SetDialogue(messageText, msg);
    }

    public void FinishDialogue()
    {
        Debug.Log("hide");
        dialogueBox.SetDialogue(messageText, " ");
        dialogueAnimation.SetTrigger("Deactivate");
    }
}
