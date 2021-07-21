using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject settingPanel;
    public GameObject fadeSprite;

    public GameObject BackgroundMusic;
    AudioSource backmusic;
    public Button audioButton;
    Image buttonImage;
    public Sprite SoundOnImage;
    public Sprite SoundOffImage;

    public static bool AudioPlay = true; //  오디오(효과음 포함)가 켜져있으면 true

    private void Awake()
    {
        SpawnManager.MobStartNum = 0;
        buttonImage = audioButton.GetComponent<Image>();
        if (AudioPlay == true) //  오디오가 켜져있으면
        {
            buttonImage.sprite = SoundOnImage;
            backmusic = BackgroundMusic.GetComponent<AudioSource>();
            if (backmusic.isPlaying) return;
            else
            {
                backmusic.Play();
            }
        }
        else // 오디오가 꺼져있으면
        {
            buttonImage.sprite = SoundOffImage;
        }
    }
    private void Start()
    {
        exitPanel.SetActive(false); // 패널 숨기기
        fadeSprite.SetActive(false);
        settingPanel.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape)) // 스마트폰 뒤로가기 버튼 눌렀을 때
        {
            exitPanel.SetActive(true); // 패널 보이기
            fadeSprite.SetActive(true);
        }
    }
}
