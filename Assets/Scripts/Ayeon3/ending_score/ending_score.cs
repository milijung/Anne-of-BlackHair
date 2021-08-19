using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Berry No Count Ending //

public class ending_score : MonoBehaviour
{
    GameObject _ending_score;
    GameObject _berry_number;

    int _berry_number_;
    int _ending_score_;

    // Start is called before the first frame update
    void Start()
    {
        _ending_score = GameObject.Find("Score");
        _ending_score.GetComponent<TextMeshProUGUI>().text = _ending_score_.ToString();
        _ending_score_ = PlayerPrefs.GetInt("Score_SE");

        _berry_number = GameObject.Find("BerryNumber");
        _berry_number.GetComponent<TextMeshProUGUI>().text = "0";
        _berry_number_ = 0;

        SaveScoreBoard();
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
