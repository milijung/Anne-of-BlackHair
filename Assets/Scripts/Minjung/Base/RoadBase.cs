using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBase : MonoBehaviour
{
    int LineNum;
    float posX, riverPosX;
    public static bool jump = false;
    public Vector2 StartPosition;
    public GameObject river, bridge, catBerry;


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
                if (gameObject.tag == "catMove")
                {
                    StartCoroutine(catRun());
                }
                else if (gameObject.tag == "bridge")
                {
                    if (SpawnManager.isforest)
                    {
                        riverPosX = 0;
                    }
                    else
                    {
                        #region bridge position X
                        LineNum = UnityEngine.Random.Range(0, 3);
                        if (LineNum == 0)
                        {
                            riverPosX = -1.35f;
                        }
                        if (LineNum == 1)
                        {
                            riverPosX = 0;
                        }
                        if (LineNum == 2)
                        {
                            riverPosX = 1.35f;
                        }
                        #endregion
                    }
                    gameObject.transform.position = new Vector2(riverPosX, 8);
                    gameObject.SetActive(true);
                    river.transform.position = new Vector2(0, 8);
                    river.SetActive(true);
                }
                else if (gameObject.tag == "river")
                {
                    if (SpawnManager.isforest)
                    {
                        riverPosX = 0;
                    }
                    else
                    {
                        #region bridge position X
                        LineNum = UnityEngine.Random.Range(0, 3);
                        if (LineNum == 0)
                        {
                            riverPosX = -1.35f;
                        }
                        if (LineNum == 1)
                        {
                            riverPosX = 0;
                        }
                        if (LineNum == 2)
                        {
                            riverPosX = 1.35f;
                        }
                        #endregion
                    }
                    gameObject.transform.position = new Vector2(0, 8);
                    gameObject.SetActive(true);
                    bridge.transform.position = new Vector2(riverPosX, 8);
                    bridge.SetActive(true);
                }
                else
                {
                    if (SpawnManager.isforest)
                    {
                        posX = 0;
                    }
                    else
                    {
                        #region position X
                        LineNum = UnityEngine.Random.Range(0, 3);
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
                    }
                    gameObject.transform.position = new Vector2(posX, 8);
                    gameObject.SetActive(true);
                }
            }
                   
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
            else if(gameObject.tag == "river")
            {
                if (collision.transform.position.x * bridge.transform.position.x<=0 && collision.transform.position.x!= bridge.transform.position.x)
                    BerryController.BumpOntheRoad = true;
            }
            else if(gameObject.tag == "catMove")
            {
                if (!jump)
                {
                    BerryController.BumpOntheRoad = true;
                    BerryController.BumpWithCat = true;
                    catBerry.SetActive(true);
                }   
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
            if (gameObject.tag != "catMove")
            {
                if (gameObject.tag == "river")
                {
                    bridge.SetActive(false);
                    gameObject.SetActive(false);
                }
                else if (gameObject.tag == "Berry")
                    collision.gameObject.SetActive(false);
                else
                    gameObject.SetActive(false);
            }
        }
    }
    IEnumerator catRun()
    {   
        int dir = UnityEngine.Random.Range(0, 2);
        if (dir == 0)
        {
            gameObject.transform.position = new Vector2(-2.5f, -1);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (dir == 1)
        {
            gameObject.transform.position = new Vector2(2.5f, -1);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        while (true)
        {
            if (Math.Abs(gameObject.transform.position.x) <= 3.5f)
            {  
                if (dir == 0)
                    gameObject.transform.Translate(Vector2.right * Time.deltaTime * GameManager.instance.gameSpeed * 12);

                else if (dir == 1)
                    gameObject.transform.Translate(Vector2.left * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            }
            else
            {
                gameObject.SetActive(false);
                catBerry.SetActive(false);
                break;
            }     
            yield return null;
        }
        StopCoroutine(catRun());
    }
}
