using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject background;

    private int childNumber = 0;
    private int childFinished = 0;

    public void ActiveEnding(bool isLost, GameObject examen)
    {
        background.SetActive(true);
        if (isLost) {
            loseScreen.SetActive(true);    
        } else {
            GameObject questListSource = examen.transform.Find("QuestList").gameObject;
            GameObject questListTarget = winScreen.transform.Find("QuestList").gameObject;
            List<Transform> childs = new List<Transform>();
            childNumber = questListSource.transform.childCount;
            for (int i=0; i < childNumber; i++) {
                Transform child = questListSource.transform.GetChild(i);
                childFinished += child.GetComponent<RectTransform>().Find("Checkmark").gameObject.activeSelf ? 1 : 0;
                childs.Add(child);
            }
            
            for (int i=0; i < childs.Count; i++) {
                Debug.Log("moving object: " + childs[i].name);
                childs[i].SetParent(questListTarget.GetComponent<RectTransform>(), false);
                winScreen.SetActive(true);  
            }
    
            UpdateGrade();
            //LayoutRebuilder.ForceRebuildLayoutImmediate(questListTarget.GetComponent<RectTransform>());
        }

    }

    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateGrade()
    {
        int calculateGrade = ((int)(20/childNumber)) * childFinished;
        winScreen.GetComponent<RectTransform>().Find("Grade").gameObject.GetComponent<TextMeshProUGUI>().SetText(calculateGrade + "/20");
    }
}
