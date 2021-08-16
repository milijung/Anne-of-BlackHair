using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public SideMob_Controller imageController;
    public MobBase[] sideMobs;
    public Sprite[] sprites;
    public SpriteRenderer[] spriteRenderers;
    public Vector2 StartPosition;
    public SomoonGauge somoon;

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

        Image_Control();
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
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = sideMobs[i].sprites[0];
        }
    }

    private void MakeFalse()
    {
        imageController.isSurprise = false;
    }

    public void Image_Control()
    {
        if (imageController.isSurprise)
        {
            for(int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].sprite = sideMobs[i].sprites[1];
            }


            
            Invoke("MakeFalse", 2f);
            Invoke("NotSurprise", 2f);
        }
    }
}
