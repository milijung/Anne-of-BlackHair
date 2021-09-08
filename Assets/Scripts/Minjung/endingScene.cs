using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Berry Count Ending //

public class endingScene : MonoBehaviour
{
    GameObject _ending_score;
    GameObject _berry_number;
    public GameObject berryPrefab;  //Áö¿ø
    public GameObject[] Berry;
    public GameObject _best_score;
    public GameObject BestScoreAudio;
    AudioSource scoreUpAudio;

    int _berry_number_;
    int _ending_score_;
    int _best_score_;

    
    // Start is called before the first frame update
    void Start()
    {
        scoreUpAudio = GameObject.Find("ScoreCountAudio").GetComponent<AudioSource>();

        if (MainMenu.AudioPlay)
            AudioManager.BackgroundAudio.Play();

        _ending_score = GameObject.Find("Score");
        _ending_score_ = PlayerPrefs.GetInt("Score_BE");
        _ending_score.GetComponent<TextMeshProUGUI>().text = _ending_score_.ToString();


        _berry_number = GameObject.Find("BerryNumber2");
        _berry_number_ =  PlayerPrefs.GetInt("Berry_BE");
        _berry_number.GetComponent<TextMeshProUGUI>().text = _berry_number_.ToString();
        StartCoroutine(BerryScoreUP());
    }

    void Update()
    {
        if(_berry_number_ == 0){
            StopCoroutine(BerryScoreUP());
            scoreUpAudio.Stop();
            SaveScoreBoard();

            Invoke("BestScore", 2f);
        }
    }
    
    IEnumerator BerryScoreUP() {
        yield return new WaitForSeconds(1f);
        if (MainMenu.AudioPlay) scoreUpAudio.Play();
        while (_berry_number_>0){
            GameObject go = Instantiate(berryPrefab) as GameObject;
            go.transform.position = new Vector3(0.76f, -0.94f, 0);
            _ending_score_ += 20;
            _berry_number_--;
            _berry_number.GetComponent<TextMeshProUGUI>().text = _berry_number_.ToString();
            _ending_score.GetComponent<TextMeshProUGUI>().text = _ending_score_.ToString();
            //
            //_berry_number_--;
            yield return new WaitForSeconds(0.4f);
        }
        //yield return new WaitForSeconds(10f);
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
        for (int i = 0; i < 3; i++)
        {
            Berry[i].SetActive(false);
        }
        _best_score_ = PlayerPrefs.GetInt("BestScore");
        _best_score.GetComponent<TextMeshProUGUI>().text = _best_score_.ToString();
        _best_score.SetActive(true);
        if(_ending_score_ == PlayerPrefs.GetInt("BestScore"))
        {
            GameObject panpare = GameObject.Find("Panpare");
            panpare.SetActive(true);
            if (MainMenu.AudioPlay) BestScoreAudio.GetComponent<AudioSource>().Play();
        }
    }
}
