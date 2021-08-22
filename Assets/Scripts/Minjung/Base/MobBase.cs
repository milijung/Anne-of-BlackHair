using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public Vector2 StartPosition;
    public SomoonGauge somoon;
    public GameObject RadarSound;
    public Sprite Surprise;
    public Sprite RadarIMG;
    public Sprite[] sprites;
    AudioSource radarSound;
    public Animator animator;

    GameObject lip_move;
    Animator _player_animator;

    private void Awake()
    {
        radarSound = RadarSound.GetComponent<AudioSource>();
        RadarIMG = gameObject.GetComponent<SpriteRenderer>().sprite;
        _player_animator = GameObject.Find("Player").GetComponent<Animator>();
    }
    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        if (SpawnManager.MobStartNum == 0)
        {
            gameObject.SetActive(false); // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
        }
        else
        {
            gameObject.SetActive(true);
            if (animator.GetBool("isErase"))
            {
                StartCoroutine(RadarOff());
            }
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
                gameObject.GetComponent<SpriteRenderer>().sprite = RadarIMG;
                gameObject.SetActive(false);
            }
            
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if(_player_animator.GetBool("MJ")) // IF MOO JUCK STATE
                return;
            
            else if(_player_animator.GetBool("SMJ"))
            {
                // SMALL MOO JUCK STATE
                return;
            } 

            SpriteRenderer SideMob = gameObject.GetComponent<SpriteRenderer>();
            SideMob.sprite = Surprise;
            if (MainMenu.AudioPlay)
                radarSound.Play();
            bool isAdult = gameObject.name.Contains("Adult");
            bool isChildren = gameObject.name.Contains("Children");

            if (isAdult && somoon.adultTouch_Num == 0)
            {
                somoon.adultTouch_Num++;
                somoon.adultFirstTouchTime = somoon.realTime;
            }

            if (isAdult && somoon.adultTouch_Num != 0)
            {
                somoon.adultTouch_Num++;
            }

            if (isChildren && somoon.childTouch_Num == 0)
            {
                somoon.childTouch_Num++;
                somoon.childFirstTouchTime = somoon.realTime;
            }

            if (isChildren && somoon.childTouch_Num != 0)
            {
                somoon.childTouch_Num++;
            }

            // lip move animation
            lip_move.GetComponent<Animator>().SetBool("L",true);

        }
        else if (collision.tag != "catMove")
            gameObject.SetActive(false); 
    }
    IEnumerator RadarOff()
    {
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = false;
        yield return new WaitForSeconds(1f);
        collider.enabled = true;
        StopCoroutine(RadarOff());
    }
}
