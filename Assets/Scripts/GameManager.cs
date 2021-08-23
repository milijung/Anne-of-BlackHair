using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreTxt; // ���� Text
    public static int score = -1;

    public float gameSpeed;
    #region instance
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        SpawnManager.MobStartNum = 0;
    }
    #endregion
    private void Start()
    {
        GamePlay();
    }
    private void Update()
    {
        scoreTxt.text = score.ToString(); // score ���� Text ��������
    }

    public IEnumerator AddScore()
    {
        while (Time.timeScale == 1) // ������ �������̸�
        {
            score++;
            yield return new WaitForSeconds(gameSpeed); // ���� �ӵ� ������ ������ ����
        }
    }

    public void GamePlay()
    {
        score = -1;
        SpawnManager.MobStartNum = 0;
        StartCoroutine(AddScore()); // score++ ����
    }
    public void GameOver()
    {
        StopCoroutine(AddScore()); // score++ ����
        Debug.Log("Game Over!");

    }
}
