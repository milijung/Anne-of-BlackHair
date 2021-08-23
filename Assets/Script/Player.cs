using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float AdultTime;     //어른에게 처음 들킨 시간
    public float ChildTime;     //아이에게 처음 들킨 시간
    float startTime;        //게임 진행 시간을 나타내기 위해 필요한 시작시간
    public float RealTime;      //게임 진행 시간

    public float SomoonGauge;    //소문게이지가 얼마나 찼는지
    public int AdultTouch;      //어른과 부딪힌 횟수
    public int ChildTouch;      //아이와 부딪힌 횟수

    public float maxSpeed;    //플레이어의 최대 스피드

    
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    Rigidbody2D rigid;

    void Awake()
    {
        startTime = Time.time;

        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        //게임진행시간 업데이트
        RealTime = Time.time - startTime;

        //Stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        SomoonCtl();
    }

    void FixedUpdate()
    {
        //이동
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * 2, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //f=0.55*(a+2b) + 0.1(t-(처음 어른에게 발각된 시간)) + 0.2(t-(처음 아이에게 발각된 시간))
        if (collision.gameObject.tag == "Enemy")
        {
            //부딪힌 게 어른인지 아이인지 구별
            bool isAdult = collision.gameObject.name.Contains("Adult");
            bool isChildren = collision.gameObject.name.Contains("Children");

            //어른과 처음 부딪쳤을 경우
            if (isAdult && AdultTouch == 0)
            {
                AdultTouch++;
                AdultTime = RealTime;  //처음 부딪힌 시간 저장
                
            }

            //어른과 n 번째 부딪힌 경우
            else if (isAdult && AdultTouch != 0)
            {
                AdultTouch++;
                
            }

            //아이와 처음 부딪쳤을 경우
            else if (isChildren && ChildTouch == 0)
            {
                ChildTouch++;
                ChildTime = RealTime;
                
            }

            //아이와 n 번째 부딪힌 경우
            else if (isChildren && ChildTouch != 0)
            {
                ChildTouch++;
                
            }
        }
    }

    //플레이어 죽는 함수
    public void OnDie()
    {
        Debug.Log("죽었습니다");
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        capsuleCollider.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
    }

    public void SomoonCtl()     //소문게이지 컨트롤 함수
    {

        if (SomoonGauge >= 100.0f)  //소문게이지가 100에 도달하면 플레이어 죽음
        {
            OnDie();
        }
        else
        {
            //계수 바꿈
            SomoonGauge = 0.5f * (AdultTouch + ChildTouch * 2);
            
            if (AdultTouch != 0)
            {
                SomoonGauge += 0.5f * (RealTime - AdultTime);
                Debug.Log("어른이 소문 퍼뜨려요");
            }
                

            if (ChildTouch != 0)
            {
                SomoonGauge += 0.8f * (RealTime - ChildTime);
                Debug.Log("아이가 소문 퍼뜨려요");
            }
                

        }

        //소문게이지 위험 경보
        if (SomoonGauge > 70 && SomoonGauge < 72)
        {
            Debug.Log("소문게이지가 70입니다!");
        }
            
        else if (SomoonGauge > 80 && SomoonGauge < 82)
            Debug.Log("소문게이지가 80입니다!");
        else if (SomoonGauge > 90 && SomoonGauge < 92)
            Debug.Log("소문게이지가 90입니다!");
        /*
        else
        {
            gameManager.ReturnDisplay();
        }
        */

    }

    
}
