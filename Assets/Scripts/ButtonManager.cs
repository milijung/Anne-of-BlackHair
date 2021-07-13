using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject gamePanel; // 패널 인스턴스
    public GameObject fadeSprite; // 패널 제외한 화면부분 어두워지게 하는 역할
    

    public void Stop() // 일시정지 버튼 눌렀을 때
    {
        gamePanel.SetActive(true);
        fadeSprite.SetActive(true);
        StopCoroutine(GameManager.AddScore());
        Time.timeScale = 0;
    }
    public void Continue() // 다시 게임 시작 버튼 눌렀을 때
    {
        Time.timeScale = 1;
        gamePanel.SetActive(false);
        fadeSprite.SetActive(false);
    }
}
