using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowQuestList : MonoBehaviour
{
    public string[] questList;
    public GameObject QuestPrefab;
    private GameObject questListObject;
    private Text text;
    private GameObject[] questions;

    // Start is called before the first frame update
    void Start()
    {
        questListObject = GameObject.Find("QuestList"); 
        if (questListObject != null) 
        {
            Debug.Log("hehe");
        }

        questions = new GameObject[questList.Length];
        float questionsSize = 100 / questList.Length;

        for(int i = 0; i < questions.Length; i++) 
        {
            GameObject question = Instantiate(QuestPrefab, questListObject.transform);
            question.name = "Q"+i;
            RectTransform questionTransform = question.GetComponent<RectTransform>();
            questionTransform.Find("Content").gameObject.GetComponent<TextMeshProUGUI>().SetText("Question n°" + (i+1) + ": " + questList[i]);
            questions[i] = question;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskCleared(int taskNumber) 
    {
        questions[taskNumber].GetComponent<RectTransform>().Find("Checkmark").gameObject.SetActive(true);
    }
}
