using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public Vector2 StartPosition;
    public SomoonGauge somoon;
    public GameObject RadarSound;
    public bool isSurprise;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    AudioSource radarSound;

    GameObject lip_move;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        radarSound = RadarSound.GetComponent<AudioSource>();
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
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Set_img();
            if(MainMenu.AudioPlay)
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
        else
            return;
    }

    private void Set_img()
    {
        spriteRenderer.sprite = sprites[1];
        Invoke("UnSet_img", 2f);
    }

    private void UnSet_img()
    {
        spriteRenderer.sprite = sprites[0];
    }
}
