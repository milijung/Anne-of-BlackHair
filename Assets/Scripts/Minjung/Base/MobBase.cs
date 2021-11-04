using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public Vector2 StartPosition;
    public SomoonGauge somoon;
    public GameObject RadarSound;
    public Sprite[] sprites;
    AudioSource radarSound;
    public Animator animator;

    GameObject lip_move;
    Animator _player_animator;

    private void Awake()
    {
        radarSound = RadarSound.GetComponent<AudioSource>();
        _player_animator = GameObject.Find("Player").GetComponent<Animator>();
        if(gameObject.GetComponent<Animator>() != null) animator = gameObject.GetComponent<Animator>();
    }
    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        if (SpawnManager.MobStartNum == 0)
        {
            gameObject.SetActive(false); // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
        }
        else
        {
            
            if(animator != null) animator.SetBool("isTouch", false);
            if (animator == null) gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            gameObject.SetActive(true);
        }
        transform.position = StartPosition;
        
        lip_move = GameObject.Find("lip_move");
    }
    private void Update()
    {
        if (GameManager.isPlay)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.gameSpeed * 12);
            
            if (transform.position.y < -8) 
            {
                if (animator != null) animator.SetBool("isErase", false);
                gameObject.SetActive(false);
            }
            
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (_player_animator.GetBool("MJ")) // IF MOO JUCK STATE
                return;

            else if (_player_animator.GetBool("SMJ"))
            {
                // SMALL MOO JUCK STATE
                return;
            }
            else
            {
                if (MainMenu.AudioPlay) radarSound.Play();

                if (animator != null) animator.SetBool("isTouch", true);
                else gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                bool isAdult = gameObject.name.Contains("Adult");
                bool isChildren = gameObject.name.Contains("Children");

                if (isAdult)
                {
                    somoon.adultTouch_Num++;
                    if (somoon.adultTouch_Num > 1) return;
                    else somoon.adultFirstTouchTime = somoon.realTime;
                }

                else // isChildren
                {
                    somoon.childTouch_Num++;
                    if (somoon.childTouch_Num > 1) return;
                    else somoon.childFirstTouchTime = somoon.realTime;
                }

                // lip move animation
                lip_move.GetComponent<Animator>().SetBool("L", true);
            }

        }
        else
        {
            if (gameObject.transform.position.y > 6)
            {
                if (collision.tag != "catMove")
                    gameObject.SetActive(false);
            }
        }
    }
}
