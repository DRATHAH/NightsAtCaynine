using UnityEngine;
using UnityEngine.Events;

public class CharacterInfo : MonoBehaviour
{
    [Tooltip("Dialogue data for the character")]
    public CharDialogueData dialogueData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueSequence testLeaf = new DialogueSequence("I just said hello");

        testLeaf.Process();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestAction()
    {
        Debug.Log("hi");
    }
}
