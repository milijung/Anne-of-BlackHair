using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomoonGauge : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Emergency;

    public Animator animator;
    public static bool somoonContinue;
    public bool isEmergency;

    public float adultFirstTouchTime;
    public float childFirstTouchTime;
    float startTime;
    public float realTime;

    public float somoonGauge;
    public GameObject lip_move;
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
        if (animator.GetInteger("State") >= 8)
        {
            if (animator.GetBool("G")) somoonContinue = true;
            else somoonContinue = false;
        }
        else
        {
            somoonContinue = true;
        }

        SomoonCtrl();
    }

    public void SomoonCtrl()
    {
        if (GameManager.isPlay && somoonGauge >= 100.0f)
        {
            GameManager.instance.GameOver();
        }

        else if (somoonGauge < 100.0f && somoonContinue || lip_move.GetComponent<Animator>().GetBool("L"))
        {
            somoonGauge = 10f * (adultTouch_Num + childTouch_Num * 1.3f);

            if (adultTouch_Num != 0)
            {
                somoonGauge += 0.3f * (realTime - adultFirstTouchTime);
            }

            if (childTouch_Num != 0)
            {
                somoonGauge += 0.3f * (realTime - childFirstTouchTime);
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
        if (adultTouch_Num != 0) adultFirstTouchTime = Time.time;
        if (childTouch_Num != 0) childFirstTouchTime = Time.time;
        if (adultTouch_Num != 0) adultTouch_Num *= 0.5f;
        if (childTouch_Num != 0) childTouch_Num *= 0.5f;
    }
}
