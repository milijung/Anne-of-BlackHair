using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public Vector2 StartPosition;
    public SomoonGauge somoon;

    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        if (SpawnManager.MobStartNum == 0)
        {
            gameObject.SetActive(false); // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
        }
        else
        {
            gameObject.SetActive(true);
        }
        transform.position = StartPosition;
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
            //�ε��� �� ����� �������� ����
            bool isAdult = gameObject.name.Contains("Adult");
            bool isChildren = gameObject.name.Contains("Children");
            //��� ó�� �ε����� ���
            if (isAdult && somoon.adultTouch_Num == 0)
            {
                somoon.adultTouch_Num++;
                somoon.adultFirstTouchTime = somoon.realTime;  //ó�� �ε��� �ð� ����
            }

            //��� n��° �ε�ģ ���
            else if (isAdult && somoon.adultTouch_Num != 0)
            {
                somoon.adultTouch_Num++;
            }

            //���̿� ó�� �ε����� ���
            else if (isChildren && somoon.childTouch_Num == 0)
            {
                somoon.childTouch_Num++;
                somoon.childFirstTouchTime = somoon.realTime;  //ó�� �ε��� �ð� ����

            }

            //���̿� n ��° �ε��� ���
            else if (isChildren && somoon.childTouch_Num != 0)
            {
                somoon.childTouch_Num++;
            }

        }
    }
}
