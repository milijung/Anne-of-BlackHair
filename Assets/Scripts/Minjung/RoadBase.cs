using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBase : MonoBehaviour
{
    int LineNum;
    float posX;
    public static bool jump = false;
    public Vector2 StartPosition;

    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        if (gameObject.tag == "BerryBox")
        {
            transform.position = StartPosition;
        }
        else
        {
            if (SpawnManager.MobStartNum == 0)
            {
                gameObject.SetActive(false); // SpawnManager ���� ���� Mob�� �����ϴ� �� ����

            }
            else
            {
                gameObject.SetActive(true);
            }
            #region position X
            LineNum = Random.Range(0, 3);
            if (LineNum == 0)
            {
                posX = -1.4f;
            }
            if (LineNum == 1)
            {
                posX = 0;
            }
            if (LineNum == 2)
            {
                posX = 1.4f;
            }
            #endregion

            transform.position = new Vector2(posX, 8);
        }
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
            
            if (gameObject.tag == "BerryBox") // 열매주머니 획득
            {
                gameObject.SetActive(false);
                BerryController.getBerryBox = true;
            }
            else if(gameObject.tag == "Berry")
            {
                gameObject.SetActive(false);
                BerryController.getBerry = true;
            }
            else
            {
                if (!jump) 
                {
                    BerryController.BumpOntheRoad = true;
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
