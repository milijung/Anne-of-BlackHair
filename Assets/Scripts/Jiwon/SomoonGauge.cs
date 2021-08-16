using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomoonGauge : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Emergency;
    public GameObject EmergencyCar;
    public GameObject Lip;

    public float adultFirstTouchTime;
    public float childFirstTouchTime;
    float startTime;
    public float realTime;

    public float somoonGauge;
    public int adultTouch_Num;
    public int childTouch_Num;

    private void Awake()
    {
        Emergency.SetActive(false);
        EmergencyCar.SetActive(false);
        startTime = Time.time;
        somoonGauge = 0;

    }

    void Update()
    {
        //게임진행시간 업데이트
        realTime = Time.time - startTime;
        if ((adultTouch_Num == 0 && childTouch_Num == 0)|| !GameManager.isPlay)
            Lip.SetActive(false);
        else
            Lip.SetActive(true);
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
            somoonGauge = 10f * (adultTouch_Num + childTouch_Num * 2);

            if(adultTouch_Num != 0)
            {
                somoonGauge += 0.1f * (realTime - adultFirstTouchTime);
            }

            if(childTouch_Num != 0)
            {
                somoonGauge += 0.1f * (realTime - childFirstTouchTime);
            }

            if (somoonGauge > 85 && somoonGauge < 88)
            {
                Emergency.SetActive(true);
                EmergencyCar.SetActive(true);
            }
        }
    }
}
