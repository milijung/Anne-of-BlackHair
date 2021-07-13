using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreTxt; // 점수 Text
    public static int score = -1;
    public static float AddScoreNum = 0.1f; // 몇초단위로 점수를 1씩 더할 것인지 결정
    private void Start()
    {
        GamePlay();
    }
    private void FixedUpdate()
    {
        scoreTxt.text = score.ToString(); // score 값을 Text 내용으로
    }

    public static IEnumerator AddScore()
    {
        while (Time.timeScale == 1) // 게임이 진행중이면
        {
            score++;
            yield return new WaitForSeconds(AddScoreNum);
        }
    }

    public void GamePlay()
    {
        score = -1;
        StartCoroutine(AddScore()); // score++ 실행
    }
    public void GameOver()
    {
        StopCoroutine(AddScore()); // score++ 멈춤
    }
}
