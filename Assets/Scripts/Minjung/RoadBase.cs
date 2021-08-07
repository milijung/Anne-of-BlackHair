using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBase : MonoBehaviour
{
    int LineNum;
    float posX;
    bool jump = false;

    private void Start()
    {
        StartCoroutine(Jump());
    }
    
    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        if (SpawnManager.MobStartNum == 0 || GameManager.score < 50)
        {
            gameObject.SetActive(false); // SpawnManager ���� ���� Mob�� �����ϴ� �� ����

        }
        else
        {
            gameObject.SetActive(true);
        }
        #region position X
        LineNum = Random.Range(0, 3);
        if (LineNum == 0)
        {
            posX = -1.4f;
        }
        if (LineNum == 1)
        {
            posX = 0;
        }
        if(LineNum == 2)
        {
            posX = 1.4f;
        }
        #endregion

        transform.position = new Vector2(posX, 8);
    }

    private void Update()
    {    
        if (GameManager.isPlay)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            if (transform.position.y < -8) // ȭ�� ������ Mob�� �̵��ϸ� �ش� Mob ��Ȱ��ȭ
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (!jump)
                Debug.Log("��ֹ� �浹");

            else
                return;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator Jump()
    {
        while (true)
        {
            if (SwipeManager.swipeUp == true)
            {
                jump = true;
                yield return new WaitForSeconds(2); // ���� �ִϸ��̼��� ������ ���� �ð� ������ ����
                jump = false;
            }
            yield return null;
        }
    }
}
