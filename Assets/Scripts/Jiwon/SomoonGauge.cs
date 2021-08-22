using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomoonGauge : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Emergency;

    public Animator animator;
    public bool somoonContinue;
    public bool isEmergency;

    public float adultFirstTouchTime;
    public float childFirstTouchTime;
    float startTime;
    public float realTime;

    public float somoonGauge;
    public float adultTouch_Num;
    public float childTouch_Num;

    private void Awake()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();

        somoonContinue = true;
        isEmergency = false;
        Emergency.SetActive(false);

        startTime = Time.time;
        somoonGauge = 0;

    }

    void Update()
    {
        //��������ð� ������Ʈ
        realTime = Time.time - startTime;

        SomoonCtrl();
    }


    public void SomoonCtrl()
    {
        //Ʈ��Ŭ ������ �� somoonContinue false ����
        if (animator.GetInteger("State") >= 8)
            somoonContinue = false;
        else
        {

            if (somoonGauge >= 100.0f)
            {
                GameManager.instance.GameOver();
            }

            else if (somoonGauge < 100.0f && somoonContinue == true)
            {
                somoonGauge = 10f * (adultTouch_Num + childTouch_Num * 2);

                if (adultTouch_Num != 0)
                {
                    somoonGauge += 0.1f * (realTime - adultFirstTouchTime);
                }

                if (childTouch_Num != 0)
                {
                    somoonGauge += 0.1f * (realTime - childFirstTouchTime);
                }

                if ((somoonGauge > 70 && somoonGauge < 100) && !isEmergency)
                {
                    OnEmergency();
                    Invoke("OffEmergency", 3.2f);
                }
                else if (somoonGauge < 70)
                    isEmergency = false;
            }

        }
    }
    private void OnEmergency()
    {

        Emergency.SetActive(true);
        isEmergency = true;
    }

    private void OffEmergency()
    {
        Emergency.SetActive(false);
    }

    public void LowerSomoon()
    {
        realTime *= 0.5f;
        adultFirstTouchTime *= 0.5f;
        childFirstTouchTime *= 0.5f;
        adultTouch_Num *= 0.5f;
        childTouch_Num *= 0.5f;

    }
}
