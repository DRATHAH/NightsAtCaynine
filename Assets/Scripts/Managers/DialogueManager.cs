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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        Debug.Log("Go to next line");
    }

    public void Choice()
    {
        Debug.Log("Show new dialogue options");
    }
}
