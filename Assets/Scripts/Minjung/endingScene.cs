using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class endingScene : MonoBehaviour
{
    GameObject _ending_score;
    GameObject _berry_number;

    
    // Start is called before the first frame update
    void Start()
    {

        if (MainMenu.AudioPlay)
            AudioManager.BackgroundAudio.Play();

        _ending_score = GameObject.Find("Score");
        _ending_score.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
    
        _berry_number = GameObject.Find("BerryNumber");
        _berry_number.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Berry").ToString();
    }
}
