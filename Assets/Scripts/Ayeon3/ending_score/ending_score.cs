using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Berry No Count Ending //

public class ending_score : MonoBehaviour
{
    GameObject _ending_score;
    GameObject _berry_number;
    public GameObject[] Berry;
    public GameObject _best_score;
    public GameObject BestScoreAudio;

    int _berry_number_;
    int _ending_score_;
    int _best_score_;

    // Start is called before the first frame update
    void Start()
    {
        if (MainMenu.AudioPlay)
            AudioManager.BackgroundAudio.Play();

        _ending_score = GameObject.Find("Score");
        _ending_score_ = PlayerPrefs.GetInt("Score_SE");
        _ending_score.GetComponent<TextMeshProUGUI>().text = _ending_score_.ToString();

        _berry_number = GameObject.Find("BerryNumber");
        _berry_number_ = 0;
        _berry_number.GetComponent<TextMeshProUGUI>().text = _berry_number_.ToString();
        
        SaveScoreBoard();
    }

    private void Update()
    {
        Invoke("BestScore", 2f);
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

    private void BestScore()
    {
        for (int i = 0; i < 4; i++)
        {
            Berry[i].SetActive(false);
        }
        _best_score_ = PlayerPrefs.GetInt("BestScore");
        _best_score.GetComponent<TextMeshProUGUI>().text = _best_score_.ToString();
        _best_score.SetActive(true);
        if (_ending_score_ == PlayerPrefs.GetInt("BestScore"))
        {
            GameObject panpare = GameObject.Find("Panpare");
            panpare.SetActive(true);
            if (MainMenu.AudioPlay) BestScoreAudio.GetComponent<AudioSource>().Play();
        }

    }
}
