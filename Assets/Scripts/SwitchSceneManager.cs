using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("mainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

}
