using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public Vector2 StartPosition;
    public SomoonGauge somoon;

    private void OnEnable() // 오브젝트가 활성화되면 실행
    {
        if (SpawnManager.MobStartNum == 0)
        {
            gameObject.SetActive(false); // SpawnManager 실행 전에 Mob이 등장하는 것 방지

        }
        else
        {
            gameObject.SetActive(true);
        }
        transform.position = StartPosition;
    }
    private void Update()
    {
        if (GameManager.isPlay || GameManager.gameOver)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            if (transform.position.y < -8) // 화면 끝까지 Mob이 이동하면 해당 Mob 비활성화
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //부딪힌 게 어른인지 아이인지 구별
            bool isAdult = gameObject.name.Contains("Adult");
            bool isChildren = gameObject.name.Contains("Children");
            //어른과 처음 부딪쳤을 경우
            if (isAdult && somoon.adultTouch_Num == 0)
            {
                somoon.adultTouch_Num++;
                somoon.adultFirstTouchTime = somoon.realTime;  //처음 부딪힌 시간 저장
            }

            //어른과 n번째 부딪친 경우
            else if (isAdult && somoon.adultTouch_Num != 0)
            {
                somoon.adultTouch_Num++;

            }

            //아이와 처음 부딪쳤을 경우
            else if (isChildren && somoon.childTouch_Num == 0)
            {
                somoon.childTouch_Num++;
                somoon.childFirstTouchTime = somoon.realTime;  //처음 부딪힌 시간 저장

            }

            //아이와 n 번째 부딪힌 경우
            else if (isChildren && somoon.childTouch_Num != 0)
            {
                somoon.childTouch_Num++;
            }

        }
    }
}
