using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomoonGauge : MonoBehaviour
{
    public GameManager gameManager;
    public float adultFirstTouchTime;
    public float childFirstTouchTime;
    float startTime;
    public float realTime;

    public float somoonGauge;
    public int adultTouch_Num;
    public int childTouch_Num;

    // Update is called once per frame
    void Awake()
    {
        startTime = Time.time;
        somoonGauge = 0;


    }

    void Update()
    {
        //게임진행시간 업데이트
        realTime = Time.time - startTime;
        SomoonCtrl();
        /*if (Input.GetKey(KeyCode.P) && GameManager.isPlay == true)
        {
            somoonGauge += 1;
        }
        if (somoonGauge >= 100.0f)
        {
            gameManager.GameOver();
        }*/ //소문게이지 테스트용 코드. 키보드 P를 누르면 소문게이지가 올라감
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Radar" && GameManager.isPlay == true)
        {
            //부딪힌 게 어른인지 아이인지 구별
            bool isAdult = collision.gameObject.name.Contains("Adult");
            bool isChildren = collision.gameObject.name.Contains("Children");

            //어른과 처음 부딪쳤을 경우
            if (isAdult && adultTouch_Num == 0)
            {
                adultTouch_Num++;
                adultFirstTouchTime = realTime;  //처음 부딪힌 시간 저장

            }

            //어른과 n번째 부딪친 경우
            else if (isAdult && adultTouch_Num != 0)
            {
                adultTouch_Num++;

            }

            //아이와 처음 부딪쳤을 경우
            else if (isChildren && childTouch_Num == 0)
            {
                childTouch_Num++;
                childFirstTouchTime = realTime;  //처음 부딪힌 시간 저장

            }

            //아이와 n 번째 부딪힌 경우
            else if (isChildren && childTouch_Num != 0)
            {
                childTouch_Num++;

            }
        }
    }

    public void SomoonCtrl()
    {
        if(somoonGauge >= 100.0f)
        {
            gameManager.GameOver();
        }
        else
        {
            somoonGauge = 0.5f * (adultTouch_Num + childTouch_Num * 2);

            if(adultTouch_Num != 0)
            {
                somoonGauge += 0.5f * (realTime - adultFirstTouchTime);
            }

            if(childTouch_Num != 0)
            {
                somoonGauge += 0.8f * (realTime - childFirstTouchTime);
            }
        }
    }
}
