using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public SideMob_Controller imageController;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    public Vector2 StartPosition;
    public SomoonGauge somoon;

    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (SpawnManager.MobStartNum == 0)
        {
            gameObject.SetActive(false); // SpawnManager ���� ���� Mob�� �����ϴ� �� ����
        }
        else
        {
            gameObject.SetActive(true);
        }
        transform.position = StartPosition;
    }
    private void Update()
    {
        if (GameManager.isPlay)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            if (transform.position.y < -8) 
            {
                gameObject.SetActive(false);
            }
        }

        Image_Control();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //놀란 동작
            imageController.isSurprise = true;

            bool isAdult = gameObject.name.Contains("Adult");
            bool isChildren = gameObject.name.Contains("Children");

            if (isAdult && somoon.adultTouch_Num == 0)
            {
                somoon.adultTouch_Num++;
                somoon.adultFirstTouchTime = somoon.realTime;
            }


            else if (isAdult && somoon.adultTouch_Num != 0)
            {
                somoon.adultTouch_Num++;
            }


            else if (isChildren && somoon.childTouch_Num == 0)
            {
                somoon.childTouch_Num++;
                somoon.childFirstTouchTime = somoon.realTime;

            }


            else if (isChildren && somoon.childTouch_Num != 0)
            {
                somoon.childTouch_Num++;
            }

        }
        else
            gameObject.SetActive(false);
    }

    private void NotSurprise()
    {
        imageController.isSurprise = false;
        spriteRenderer.sprite = sprites[0];
    }

    public void Image_Control()
    {
        if (imageController.isSurprise)
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("NotSurprise", 2f);
        }
    }
}
