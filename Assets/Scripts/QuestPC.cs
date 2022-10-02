using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestPC : QuestInteractable
{
    
    public float timer = 10.0f;
    private bool started;
    private bool wait = false;
    private TextMeshPro textTimer;

    public int numberOfActionToComplete = 6;
    private int nomberOfActionCompleted = 0;

    public void ChangeState() 
    {
        wait = !wait;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        CloseDialog();
    }

    void CloseDialog()
    {

    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.SetText(string.Format("{0:00}:{1:00}:{2:00}", 0, minutes, seconds));
    }
    
}
