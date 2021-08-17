using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isPlay;

    public TextMeshProUGUI scoreTxt; // 점수 Text
    public static int score = 0;

    public GameObject GameOverPanel;
    public GameObject fadeSprite;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI bestScore;
    public GameObject player;
    public GameObject AnneCry;
    public Slider rumor;
    public GameObject itemSlot;

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public GameObject stepSound;
    AudioSource stepAudio;

    public float gameSpeed;
    public GameObject[] Count;

    
    

    #region instance
    public static GameManager instance;
    private void Awake()
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        stepAudio = stepSound.GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
        if (!PlayerPrefs.HasKey("SecondScore"))
            PlayerPrefs.SetInt("SecondScore", 0);

        if (!PlayerPrefs.HasKey("ThirdScore"))
            PlayerPrefs.SetInt("ThirdScore", 0);

        GamePlay();
        gameSpeed = 0.3f;
    }
    private void Update()
    {
        scoreTxt.text = score.ToString(); // score 값을 Text 내용으로     
    }

    public IEnumerator AddScore()
    {
        while (true) 
        {
            if(Time.timeScale == 1 && isPlay) { 
                score++;
                yield return new WaitForSeconds(gameSpeed/Mathf.Pow(ItemController.upSpeed,2)); // 게임 속도 단위로 점수를 더함
            }
            yield return null;
        }
        
    }
    public IEnumerator CountDown()
    {
        for(int i = 0;i<Count.Length;i++)
        {
            Count[i].transform.position = new Vector2(-5.5f, 0);
            Count[i].SetActive(true);
            while (true)
            {
                if (Count[i].transform.position.x >= 0)
                {
                    break;
                }
                Count[i].transform.Translate(Vector2.right * 20 * Time.deltaTime);
                yield return null;
            }
            Count[i].transform.position = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                if (Count[i].transform.position.x >= 5.5f)
                {
                    break;
                }
                Count[i].transform.Translate(Vector2.right * 20 * Time.deltaTime);
                yield return null;
            }
            Count[i].SetActive(false);
            yield return new WaitForSeconds(0.5f);
            yield return null;   
        }
        isPlay = true;
        StopCoroutine(CountDown());
        yield return null;
    }
    public void Save()
    {
        if (score < PlayerPrefs.GetInt("BestScore"))
        {
            if (score < PlayerPrefs.GetInt("SecondScore"))
            {
                if (score < PlayerPrefs.GetInt("ThirdScore"))
                    return;
                PlayerPrefs.SetInt("ThirdScore", score);
            }
            PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
            PlayerPrefs.SetInt("SecondScore", score);
            return;
        }
        if (score == PlayerPrefs.GetInt("BestScore"))
            return;
        PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
        PlayerPrefs.SetInt("SecondScore", PlayerPrefs.GetInt("BestScore"));
        PlayerPrefs.SetInt("BestScore", score);
    }
    public void GamePlay()
    {
        score = 0;
        isPlay = true;
        scoreTxt.text = string.Empty;
        scoreTxt.gameObject.SetActive(true);
        player.SetActive(true);
        rumor.gameObject.SetActive(true);
        itemSlot.SetActive(true);

        SpawnManager.MobStartNum = 0;
        StartCoroutine(AddScore()); // score++ 실행

        if (MainMenu.AudioPlay == true)
        {
            backmusic.Play();
            stepAudio.Play();
        }
        else
        {
            backmusic.Pause();
            stepAudio.Pause();
        }

    }
    public void GameOver()
    {
        isPlay = false;
        BerryController.getBerryBox = false;
        StopCoroutine(AddScore()); // score++ 멈춤
        Save();
        bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        finalScore.text = score.ToString(); // 게임오버 화면 활성화
        GameOverPanel.SetActive(true);
        fadeSprite.SetActive(true);
        AnneCry.SetActive(true);

        scoreTxt.gameObject.SetActive(false);
        player.SetActive(false);
        rumor.gameObject.SetActive(false);
        itemSlot.SetActive(false);

        if (MainMenu.AudioPlay == true)
        {
            backmusic.Pause();
            stepAudio.Pause();
        }

    }



}

