using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomoonGauge : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Emergency;
    
    public Animator animator;

    public bool somoonContinue;
    public float adultFirstTouchTime;
    public float childFirstTouchTime;
    float startTime;
    public float realTime;

    public float somoonGauge;
    public int adultTouch_Num;
    public int childTouch_Num;

    private void Awake()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        Emergency.SetActive(false);
        
        startTime = Time.time;
        somoonGauge = 0;
        somoonContinue = true;

    }

    void Update()
    {
        //게임진행시간 업데이트
        realTime = Time.time - startTime;
        SomoonCtrl();
    }

    


    public void SomoonCtrl()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.RED") || animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DEEP_RED"))
        {
            somoonContinue = false;
        }

        else
        {
            somoonContinue = true;
            if (somoonGauge >= 100.0f)
            {
                gameManager.GameOver();
            }
            else if (somoonGauge < 100.0f && somoonContinue == true)
            {
                somoonGauge = 0.5f * (adultTouch_Num + childTouch_Num * 2);

                if (adultTouch_Num != 0)
                {
                    somoonGauge += 0.5f * (realTime - adultFirstTouchTime);
                }

                if (childTouch_Num != 0)
                {
                    somoonGauge += 0.8f * (realTime - childFirstTouchTime);
                }

                if ((somoonGauge > 85 && somoonGauge < 88))
                {
                    Emergency.SetActive(true);
                    
                }
                else
                {
                    Emergency.SetActive(false);
                    
                }

            }
        }        

    }
    
}
