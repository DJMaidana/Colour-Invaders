using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitApplication();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetHiScore()
    {
        PlayerPrefs.SetInt("HiScore", 0);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
