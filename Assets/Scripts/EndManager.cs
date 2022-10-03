using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject background;

    public void ActiveEnding(bool isLost, GameObject examen)
    {
        background.gameObject.SetActive(true);
        if (isLost) {
            loseScreen.gameObject.SetActive(true);    
        } else {
            GameObject questListSource = examen.transform.Find("QuestList").gameObject;
            GameObject questListTarget = winScreen.transform.Find("QuestList").gameObject;
            List<Transform> childs = new List<Transform>();
            for (int i=0; i < questListSource.transform.childCount; i++) {
                Transform child = questListSource.transform.GetChild(i);
                childs.Add(child);
            }
            
            for (int i=0; i < childs.Count; i++) {
                Debug.Log("moving object: " + childs[i].name);
                childs[i].SetParent(questListTarget.transform, false);
                winScreen.SetActive(true);  
            }
        }

    }

    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
