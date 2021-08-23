using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
    //f=0.55*(a+2b) + 0.1(t-(처음 어른에게 발각된 시간)) + 0.2(t-(처음 아이에게 발각된 시간))
    public float SomoonGage;    //소문게이지가 얼마나 찼는지
    public int AdultTouch;      //어른과 부딪힌 횟수
    public int ChildTouch;  //아이와 부딪힌 횟수
    public Player player;


    void Update()
    {
        SomoonCtr(player.realTime, player.AdultTime, player.ChildTime);
    }

    public void SomoonCtr(float realTime, float AdultTime, float ChildTime)     //소문게이지 컨트롤 함수
    {

        if (SomoonGage == 100.0f)  //소문게이지가 100에 도달하면 플레이어 죽음
        {
            player.OnDie();
        }
        else
        {
            SomoonGage = 0.55f * (AdultTouch + ChildTouch * 2)
            + 0.1f * ((realTime - AdultTime) + 0.2f * (realTime - ChildTime));
        }
    }
    
    

    void SomoonAlarm()  //소문게이지가 특정 점수에 도달할 때마다 알람
    {
        if (SomoonGage == 70)
            Debug.Log("소문게이지가 70입니다!");
        else if (SomoonGage == 80)
            Debug.Log("소문게이지가 80입니다!");
        else if  (SomoonGage == 90)
            Debug.Log("소문게이지가 90입니다!");
    }
    */
}
