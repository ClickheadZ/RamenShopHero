using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{

    //This class will contain methods to switch scenes, to be called when certain buttons are pressed

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void GoCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("IntroMenu");
    }
}
