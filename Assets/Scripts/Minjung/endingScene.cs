using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Berry Count Ending //

public class endingScene : MonoBehaviour
{
    GameObject _ending_score;
    GameObject _berry_number;

    int _berry_number_;
    int _ending_score_;

    // Start is called before the first frame update
    void Start()
    {
        if (MainMenu.AudioPlay)
            AudioManager.BackgroundAudio.Play();

        _ending_score = GameObject.Find("Score");
        _ending_score_ = PlayerPrefs.GetInt("Score_BE");
        _ending_score.GetComponent<TextMeshProUGUI>().text = _ending_score_.ToString();


        _berry_number = GameObject.Find("BerryNumber2");
        _berry_number_ =  8;//PlayerPrefs.GetInt("Berry_BE");
        _berry_number.GetComponent<TextMeshProUGUI>().text = _berry_number_.ToString();

    }

    void Update()
    {
        while(_berry_number_>0)
        {
            StartCoroutine(BerryScoreUP());
        }
        SaveScoreBoard();
    }
    
    IEnumerator BerryScoreUP() {
        yield return new WaitForSeconds(1f);
        _berry_number_--;
        _berry_number.GetComponent<TextMeshProUGUI>().text = _berry_number_.ToString();
        _ending_score_+=20;
        _ending_score.GetComponent<TextMeshProUGUI>().text = _ending_score_.ToString();
        yield return new WaitForSeconds(1f);
    }

    public void SaveScoreBoard()
    {
        // Save the SCORE BOARD
        if (_ending_score_ < PlayerPrefs.GetInt("BestScore"))
        {
            if (_ending_score_ < PlayerPrefs.GetInt("SecondScore"))
            {
                if (_ending_score_ < PlayerPrefs.GetInt("ThirdScore"))
                    return;
                PlayerPrefs.SetInt("ThirdScore", _ending_score_);
            }
            PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
            PlayerPrefs.SetInt("SecondScore", _ending_score_);
            return;
        }

        if (_ending_score_ == PlayerPrefs.GetInt("BestScore"))
            return;
        PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
        PlayerPrefs.SetInt("SecondScore", PlayerPrefs.GetInt("BestScore"));
        PlayerPrefs.SetInt("BestScore", _ending_score_);
    }
}
