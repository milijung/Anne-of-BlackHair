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
    public SideMob_Controller Mob_motion;

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
            somoonContinue = true;

        SomoonCtrl();
    }

    public void SomoonCtrl()
    {
        if (GameManager.isPlay && somoonGauge >= 100.0f)
        {
            GameManager.instance.GameOver();
        }

        else if (!somoonContinue) return;
        else
        {
            somoonGauge = 10f * (adultTouch_Num + childTouch_Num * 1.3f) + 0.3f * (realTime - adultFirstTouchTime) + 0.3f * (realTime - childFirstTouchTime);
            if (isEmergency) return;
            else if (somoonGauge <= 70) return;
            else
            {
                StartCoroutine(OnEmergency());
            }
        }
    }
    public IEnumerator OnEmergency()
    {
        isEmergency = true;
        while(true)
        {
            if (somoonGauge <= 70) break;
            else {
                Emergency.SetActive(true);
                yield return new WaitForSeconds(3.2f);
                Emergency.SetActive(false);
                yield return new WaitForSeconds(3.2f); 
            }
        }
        isEmergency = false;
        StopCoroutine(OnEmergency());
    }

    public void LowerSomoon()
    {
        if (MainMenu.AudioPlay) AudioManager.deathBerryAudio.Play();
        Mob_motion.Set();
        if (adultTouch_Num == 0 && childTouch_Num == 0) return;
        else
        {
            if (adultTouch_Num != 0)
            {
                adultFirstTouchTime = (Time.time + adultFirstTouchTime)*2 / 3;
                adultTouch_Num *= 0.7f;
            }
            if (childTouch_Num != 0)
            {
                childFirstTouchTime = (Time.time + childFirstTouchTime)*2 / 3;
                childTouch_Num *= 0.7f;
            }
            somoonGauge = 10f * (adultTouch_Num + childTouch_Num * 1.3f) + 0.3f * (realTime - adultFirstTouchTime) + 0.3f * (realTime - childFirstTouchTime);
        }
    }
}
