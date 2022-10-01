using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
    public float timer = 300.0f;
    public ProfessorBehaviour professor;
    private float standBy = 0.0f;
    private bool wait = false;
    private TextMeshPro textTimer;

    // Start is called before the first frame update
    void Start()
    {
        textTimer = transform.Find("Time").gameObject.GetComponent<TextMeshPro>();
        DisplayTime();
    }

    // Update is called once per frame
    void Update()
    {   
        if(wait && ((int)timer) % ((int)professor.watchingTime) == 0 && standBy <= professor.notWatchingTime) 
        {
            standBy += Time.deltaTime;
        } else {
            if(((int)timer) % ((int)professor.watchingTime) != 0) {
                if (!wait) 
                {
                    professor.changeStance();
                }
                wait = true;
            } else {
                if (wait) 
                {
                    professor.changeStance();
                }
                wait = false;
            }
            standBy = 0.0f;
            timer -= Time.deltaTime;
        }
        DisplayTime();
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.SetText(string.Format("{0:00}:{1:00}:{2:00}", 0, minutes, seconds));
    }

    void Exterminate() 
    {
        Debug.Log("BOOOOOM !");
    }
}
