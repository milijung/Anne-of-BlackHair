using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isPlay;
    public TextMeshProUGUI scoreTxt; // ���� Text
    public ItemController itemController;
    public static int score = 0;

    public GameObject fadeSprite;
    public GameObject SpeedUpTxt;
    public GameObject player;
    public Slider rumor;
    public GameObject itemSlot;
    public BoxCollider[] color_collider;
    public SpriteRenderer[] color_sprite;

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public GameObject stepSound;
    AudioSource stepAudio;
    public GameObject EndingAudio;

    public static float gameSpeed;
    public static int speedIndex = 0;
    float[] speed = { 0.3f, 0.5f, 0.7f, 1.5f };
    float[] scoreTerm = { 0.5f, 0.3f, 0.1f, 0.03f };
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
        speedIndex = 0;
        if (!PlayerPrefs.HasKey("BestScore"))       PlayerPrefs.SetInt("BestScore", 0);
        if (!PlayerPrefs.HasKey("SecondScore"))     PlayerPrefs.SetInt("SecondScore", 0);
        if (!PlayerPrefs.HasKey("ThirdScore"))      PlayerPrefs.SetInt("ThirdScore", 0);


        if (!PlayerPrefs.HasKey("Score_BE"))      PlayerPrefs.SetInt("Score_SE", 0);
        if (!PlayerPrefs.HasKey("Berry_BE"))      PlayerPrefs.SetInt("Score_SE", 0);

        if (!PlayerPrefs.HasKey("Score_SE"))      PlayerPrefs.SetInt("Score_SE", 0);

        player.SetActive(true);
        score = 0;
        player.GetComponent<Transform>().position = new Vector2(0, -4);
        player.GetComponent<Animator>().SetBool("START",true);
        Invoke("GamePlay",1.75f);
        StartCoroutine(ChangeSpeed());
    }
    private void Update()
    {
        gameSpeed = speed[speedIndex];
        scoreTxt.text = score.ToString(); // score ���� Text ��������
        if (player.GetComponent<Animator>().GetBool("ITEM"))
        {
            for (int i = 0; i < color_collider.Length; i++)
            {
                color_sprite[i].color = new Color(1, 1, 1, 0.4f);
                color_collider[i].enabled = false;
            }
        }
        if (!player.GetComponent<Animator>().GetBool("ITEM"))
        {
            for (int i = 0; i < color_collider.Length; i++)
            {
                color_sprite[i].color = new Color(1, 1, 1, 1);
                color_collider[i].enabled = true;
            }
        }
    }
    public IEnumerator ChangeSpeed()
    {
        while (true)
        {
            if (isPlay)
            {
                if (!ItemController.isBasket)
                {
                    if (SpawnManager.Speed_Num == 1)
                    {
                        backmusic.pitch += 0.05f;
                        SpeedUpTxt.SetActive(true);
                        yield return new WaitForSeconds(0.5f);
                        SpeedUpTxt.SetActive(false);
                        yield return new WaitForSeconds(0.2f);
                        SpeedUpTxt.SetActive(true);
                        yield return new WaitForSeconds(0.5f);
                        SpeedUpTxt.SetActive(false);
                        yield return new WaitForSeconds(0.2f);
                        while (GameManager.speedIndex == 3) yield return null;
                        GameManager.speedIndex++;
                        while (SpawnManager.Speed_Num == 1) yield return null;
                    }
                    else if(SpawnManager.Speed_Num == 5)
                    {
                        backmusic.pitch += 0.05f;
                        SpeedUpTxt.SetActive(true);
                        yield return new WaitForSeconds(0.5f);
                        SpeedUpTxt.SetActive(false);
                        yield return new WaitForSeconds(0.2f);
                        SpeedUpTxt.SetActive(true);
                        yield return new WaitForSeconds(0.5f);
                        SpeedUpTxt.SetActive(false);
                        yield return new WaitForSeconds(0.2f);
                        while (GameManager.speedIndex == 3) yield return null;
                        GameManager.speedIndex++;
                        break;
                    }
                    else {}
                }
            }
            yield return null;
        }
        StopCoroutine(ChangeSpeed());
    }

    public IEnumerator AddScore()
    {
        while (true) 
        {
            if(Time.timeScale == 1 && isPlay) { 
                score++;
                yield return new WaitForSeconds(scoreTerm[speedIndex]/Mathf.Pow(itemController.upSpeed, 2)); // ���� �ӵ� ������ ������ ����
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
                if (Count[i].transform.position.x >= 0) break;

                Count[i].transform.Translate(Vector2.right * 20 * Time.deltaTime);
                yield return null;
            }
            Count[i].transform.position = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                if (Count[i].transform.position.x >= 5.5f) break;
                
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

    public void GamePlay()
    {
        score = 0;
        isPlay = true;
        scoreTxt.text = string.Empty;
        scoreTxt.gameObject.SetActive(true);

        rumor.gameObject.SetActive(true);
        itemSlot.SetActive(true);

        SpawnManager.MobStartNum = 0;
        StartCoroutine(AddScore()); // score++ ����

        if (MainMenu.AudioPlay == true)
        {
            backmusic.pitch = 1;
            backmusic.Play();
            stepAudio.Play();
        }
        else
        {
            backmusic.Pause();
            stepAudio.Pause();
        }

    }
    
    IEnumerator GameOver2()
    {
        yield return new WaitForSeconds(2.3f);

        if (GameObject.Find("SomoonGauge").GetComponent<SomoonGauge>().somoonGauge >= 100)
        {
            // Save the Now SCORE
            PlayerPrefs.SetInt("Score_SE", score);
            PlayerPrefs.SetInt("Score_BE", score);
            
            // Save the Berry Num
            PlayerPrefs.SetInt("Berry_BE", BerryController.BerryNum);

            GameObject.Find("SwitchScene").GetComponent<SwitchScene>().SomoonGameOver();
        }
        if (BerryController.BerryNum <= 0)
        {
            // Save the Now SCORE
            PlayerPrefs.SetInt("Score_SE", score);
            PlayerPrefs.SetInt("Score_BE", score);

            // Save the Berry Num
            PlayerPrefs.SetInt("Berry_BE", BerryController.BerryNum);

            GameObject.Find("SwitchScene").GetComponent<SwitchScene>().BerryGameOver();
        }

        StopCoroutine(GameOver2());
    }

    public void GameOver()
    {
        player.GetComponent<Animator>().SetTrigger("end");
        isPlay = false;
        if (MainMenu.AudioPlay == true)
        {
            backmusic.Pause();
            stepAudio.Pause();
            EndingAudio.GetComponent<AudioSource>().Play();
        }
        BerryController.getBerryBox = false;
        StopCoroutine(AddScore()); // score++ ����
        StartCoroutine(GameOver2());
    }
}

