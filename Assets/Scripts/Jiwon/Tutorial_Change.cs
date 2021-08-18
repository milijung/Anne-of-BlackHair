using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Change : MonoBehaviour
{
    public GameObject[] Pages;
    public int pageIndex;

    private void Start()
    {
        Pages[0].SetActive(true);
        Pages[1].SetActive(false);
        Pages[2].SetActive(false);
        Pages[3].SetActive(false);
        Pages[4].SetActive(false);
    }

    public void NextPage()
    {
        if (pageIndex < Pages.Length - 1)
        {
            AudioManager.ButtonAudio.Play();
            Pages[pageIndex].SetActive(false);
            pageIndex++;
            Pages[pageIndex].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void PrePage()
    {
        if (pageIndex > 0)
        {
            AudioManager.ButtonAudio.Play();
            Pages[pageIndex].SetActive(false);
            pageIndex--;
            Pages[pageIndex].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Title");
        }
    }
}
