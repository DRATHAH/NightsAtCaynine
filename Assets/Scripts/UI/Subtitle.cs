using UnityEngine;
using TMPro;

public class Subtitle : MonoBehaviour
{
    public bool finished = false;

    TMP_Text uiText;
    string textToWrite = "";
    int characterIndex;
    float timePerCharacter;
    float timer;
    bool invisibleCharacters;

    public void AddWriter(TMP_Text text, string toWrite, float time, bool invisChars)
    {
        if (!textToWrite.Equals(toWrite) || !text.text.Contains(toWrite))
        {
            finished = false;

            uiText = text;

            textToWrite = toWrite;
            timePerCharacter = time;
            invisibleCharacters = invisChars;
            characterIndex = 0;
        }
    }

    public void ClearDialogue(TMP_Text text)
    {
        text.text = "";
        uiText = null;
    }

    public void SetDialogue(TMP_Text text, string toWrite)
    {
        if (!finished)
        {
            finished = true;
            uiText = null;
            text.text = toWrite;
        }
    }

    private void Update()
    {
        if (uiText)
        {
            timer -= Time.deltaTime; // Start timer for when to move on to the next character
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                if (textToWrite.Length <= 0) // Check to see if all letters of textToWrite have been written
                {
                    uiText = null;
                    return;
                }
                else
                {
                    string text = textToWrite.Substring(0, characterIndex); // Makes text that was written visible
                    if (invisibleCharacters)
                    {
                        text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>"; // Adds rest of textToWrite as invisible characters
                    }
                    uiText.text = text;

                    if (characterIndex >= textToWrite.Length)
                    {
                        uiText = null;
                        finished = true;
                        return;
                    }
                }
            }
        }
    }
}
