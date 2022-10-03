using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("ClassRoom", LoadSceneMode.Additive);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
