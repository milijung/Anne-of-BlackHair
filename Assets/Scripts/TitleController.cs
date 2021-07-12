using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController: MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject fadeSprite;
    private void Start()
    {
        gamePanel.SetActive(false); // 패널 숨기기
        fadeSprite.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape)) // 스마트폰 뒤로가기 버튼 눌렀을 때
        {
            gamePanel.SetActive(true); // 패널 보이기
            fadeSprite.SetActive(true);
        }
    }
}
