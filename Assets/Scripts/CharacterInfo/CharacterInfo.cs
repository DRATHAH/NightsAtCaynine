using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [Tooltip("Name of the character speaking")]
    new public string name = "Character Name";
    [Tooltip("Color of the text the character will speak in")]
    public Color dialogueColor = Color.white;
    [Tooltip("Dialogue data for the character")]
    public CharDialogueData dialogueData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
