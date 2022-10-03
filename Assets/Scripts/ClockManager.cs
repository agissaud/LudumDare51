using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
    public static ClockManager Instance { get; private set; }

    public float timer = 300.0f;
    public ProfessorBehaviour professor;
    private float watching = 0.0f;
    private bool wait = false;
    private bool isWatching = false;
    private TextMeshPro textTimer;
    public static bool isTimeStopped {get; set;}

    public GameObject canvas;

    private void Awake()
    {
        Instance = this;
        isTimeStopped = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        textTimer = transform.Find("Time").gameObject.GetComponent<TextMeshPro>();
        DisplayTime();
    }

    // Update is called once per frame
    void Update()
    {   
        if(isTimeStopped) {
            return;
        }
        
        DisplayTime();

        CheckEnd();

        if(wait && ((int)timer) % ((int)professor.notWatchingTime) == 0 && watching <= professor.watchingTime) 
        {
            Debug.Log(timer);
            watching += Time.deltaTime;
            if (!isWatching) 
            {
                professor.changeStance();
                isWatching = true;
            }
        } else {
            if (isWatching) 
            {
                professor.changeStance();
                isWatching = false;
            }

            if(((int)timer) % ((int)professor.notWatchingTime) != 0) {
                wait = true;
            } else {                
                wait = false;
            }
            watching = 0.0f;
            timer -= Time.deltaTime;
        }

    }

    void CheckEnd()
    {
        if (timer <= 0.0f) 
        {
            Exterminate(false);
        }
    }
     

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.SetText(string.Format("{0:00}:{1:00}:{2:00}", 0, minutes, seconds));
    }

    public void Exterminate(bool isLost) 
    {
        Debug.Log("THIS IS THE END !");
        Time.timeScale = 0;
        isTimeStopped = true;
        GameObject examen = canvas.GetComponent<RectTransform>().GetChild(0).gameObject;
        examen.SetActive(false);
        canvas.GetComponent<RectTransform>().GetChild(1).gameObject.SetActive(false);
        canvas.GetComponent<RectTransform>().GetChild(2).gameObject.GetComponent<EndManager>().ActiveEnding(isLost, examen);
        

        // Pour charger une nouvelle scene :
        // using UnityEngine.SceneManagement;
        // puis SceneManager.load("SceneName");
    }
}
