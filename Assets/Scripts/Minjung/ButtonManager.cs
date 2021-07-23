using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject gamePanel; // 패널 인스턴스
    public GameObject exitPanel;
    public GameObject fadeSprite; // 패널 제외한 화면부분 어두워지게 하는 역할

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public GameObject stepSound;
    AudioSource stepAudio;

    public Button audioButton;
    Image buttonImage;
    public Sprite SoundOnImage;
    public Sprite SoundOffImage;



    public void Stop() // 일시정지 버튼 눌렀을 때
    {
        gamePanel.SetActive(true);
        fadeSprite.SetActive(true);
        Time.timeScale = 0;
        GameManager.isPlay = false;
        
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        stepAudio = stepSound.GetComponent<AudioSource>();

        backmusic.Pause();
        stepAudio.Pause();
    }
    public void Continue() // 다시 게임 시작 버튼 눌렀을 때
    {
        Time.timeScale = 1;
        gamePanel.SetActive(false);
        fadeSprite.SetActive(false);
        StartCoroutine(GameManager.instance.CountDown());
        
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        stepAudio = stepSound.GetComponent<AudioSource>();
        if (MainMenu.AudioPlay)
        {
            backmusic.Play();
            stepAudio.Play();
        }


    }
    public void Setting()
    {
        gamePanel.SetActive(true);
        fadeSprite.SetActive(true);
    }
    public void Home() // MainMenu Panel의 home 버튼
    {
        gamePanel.SetActive(false);
        exitPanel.SetActive(false);
        fadeSprite.SetActive(false);
    }

    public void BGM()
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        buttonImage = audioButton.GetComponent<Image>();
        if (backmusic.isPlaying)
        {
            MainMenu.AudioPlay = false;
            buttonImage.sprite = SoundOffImage;
            backmusic.Pause();

        }
        else
        {
            MainMenu.AudioPlay = true;
            buttonImage.sprite = SoundOnImage;
            backmusic.Play();
        }
    }
}
