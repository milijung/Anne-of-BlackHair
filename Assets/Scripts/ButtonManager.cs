using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject fadeSprite;

    public void Stop() // 일시정지 버튼 눌렀을 때
    {
        gamePanel.SetActive(true);
        fadeSprite.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue() // 다시 게임 시작 버튼 눌렀을 때
    {
        gamePanel.SetActive(false);
        fadeSprite.SetActive(false);
        Time.timeScale = 1;
    }
}
