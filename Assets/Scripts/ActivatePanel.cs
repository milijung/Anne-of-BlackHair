using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePanel : MonoBehaviour
{
    public GameObject gamePanel;

    void Start()
    {
        gamePanel.SetActive(false); // 패널 숨기기
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) // 스마트폰 뒤로가기 버튼 눌렀을 때
        {
            Time.timeScale = 0; // 일시정지
            gamePanel.SetActive(true); // 패널 보이기
        }
    }
}
