using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowQuestList : MonoBehaviour
{
    public QuestManager questManager;
    public GameObject QuestPrefab;
    public GameObject questListObject;
    public GameObject grade;
    private Text text;
    private GameObject[] questions;

    private int numberOfTaskCleared = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (questListObject == null)
            questListObject = GameObject.Find("QuestList"); 

        if (questListObject != null) 
        {
            Debug.Log("hehe");
        }

        // Register for quest completion event
        questManager.OnQuestCompleted += TaskCleared;

        questions = new GameObject[questManager.QuestInstances.Count];
        float questionsSize = 100 / questManager.QuestInstances.Count;

        for(int i = 0; i < questions.Length; i++) 
        {
            GameObject question = Instantiate(QuestPrefab, questListObject.transform);
            question.name = "Q"+i;
            RectTransform questionTransform = question.GetComponent<RectTransform>();
            questionTransform.Find("Content").gameObject.GetComponent<TextMeshProUGUI>().SetText("Question n°" + (i+1) + ": " + questManager.QuestInstances[i].Data.text);
            questions[i] = question;
        }
    }

    void TaskCleared(int taskNumber) 
    {
        numberOfTaskCleared++;
        questions[taskNumber].GetComponent<RectTransform>().Find("Checkmark").gameObject.SetActive(true);
        UpdateGrade();
    }

    void UpdateGrade()
    {
        int calculateGrade = ((int)(20/questions.Length)) * numberOfTaskCleared;
        grade.GetComponent<TextMeshProUGUI>().SetText(calculateGrade + "/20");
        if (numberOfTaskCleared == questions.Length) {
            ClockManager.Instance.Exterminate(false);
        }
    }
}
