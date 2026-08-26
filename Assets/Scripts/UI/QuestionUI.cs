using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public Transform questionsParent;
    public TMP_Text questionText;
    public GameObject answerPrefab;

    public List<GameObject> answers = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAnswer(AnswerChoice answerToAdd)
    {
        GameObject newAnswer = Instantiate(answerPrefab, questionsParent);
        DialogueOption dialogueOption = newAnswer.GetComponent<DialogueOption>();

        answers.Add(newAnswer);
        dialogueOption.InitializeChoice(answerToAdd.dialogueLine, answerToAdd.charcterResponding, answerToAdd.interactionNum, answerToAdd.textNum);
    }

    public void ClearAnswers()
    {
        foreach(GameObject answer in answers)
        {
            Destroy(answer);
        }

        answers.Clear();
        gameObject.SetActive(false);
    }
}
