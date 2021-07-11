using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    public GameObject gamePanel;

    public void Stop() // 일시정지 버튼 눌렀을 때
    {
        gamePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue() // 다시 게임 시작 버튼 눌렀을 때
    {
        gamePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
