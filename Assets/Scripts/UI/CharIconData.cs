using UnityEngine;

public class CharIconData : MonoBehaviour
{
    public enum Emotion
    {
        Resting,
        Happy,
        Sad,
        Angry,
        Blush
    }

    public Emotion emotionState = Emotion.Resting;
    public CharDialogueData character;

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AssignCharacter(CharDialogueData assignedChar)
    {
        character = assignedChar;
    }

    public void UpdateVisual(Emotion newState, string charName)
    {
        if (charName.Equals(character.name))
        {
            animator.SetBool("notSpeaking", false);
        }
        else
        {
            animator.SetBool("notSpeaking", true);
        }
    }
}
