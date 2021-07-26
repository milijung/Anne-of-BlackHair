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

    private void Awake()
    {
        startTime = Time.time;
        somoonGauge = 0;

    }

    void Update()
    {
        //게임진행시간 업데이트
        realTime = Time.time - startTime;
        SomoonCtrl();
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
