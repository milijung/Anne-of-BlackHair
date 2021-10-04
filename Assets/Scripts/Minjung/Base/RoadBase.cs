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
    public Sprite catWakeUp, catSleepIMG;
    public Sprite deerStand, deerSitDown;
    public bool deerSit;
    public Camera getCamera;
    
    Animator _player_animator;

    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        _player_animator = GameObject.Find("Player").GetComponent<Animator>();

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
                else if(gameObject.tag == "deer")
                {
                    deerSit = false;
                    transform.position = StartPosition;
                    gameObject.GetComponent<SpriteRenderer>().sprite = deerStand;
                    StartCoroutine(deer());
                }
                else
                {
                    if (SpawnManager.isforest || gameObject.tag == "Berry")
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
        if (!GameManager.isPlay) return;
        else
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.gameSpeed * 12);
            if (transform.position.y >= -8) return;
            else
            {
                if (gameObject.tag == "catSleep")
                    gameObject.GetComponent<SpriteRenderer>().sprite = catSleepIMG;
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
                if (MainMenu.AudioPlay)
                    AudioManager.ButtonAudio.Play();
                gameObject.SetActive(false);
                BerryController.BerryNum += 3;
                BerryController.getBerryBox = true;
            }
            else if (gameObject.tag == "Berry")
            {
                if (MainMenu.AudioPlay)
                    AudioManager.ButtonAudio.Play();
                gameObject.SetActive(false);
                BerryController.getBerry = true;
            }
            else if (gameObject.tag == "river")
            {
                if (_player_animator.GetBool("BST")) return;
                else
                    StartCoroutine(BumpWithRiver(collision, bridge));
            }
            else if (gameObject.tag == "catMove")
            {
                if (_player_animator.GetBool("BST")) return;

                else if (jump || ItemController.isBasket) return;

                else
                {
                    if (MainMenu.AudioPlay)
                        gameObject.GetComponent<AudioSource>().Play();
                    BerryController.BumpOntheRoad = true;
                    BerryController.BumpWithCat = true;
                    catBerry.SetActive(true);
                }
            }
            else if (gameObject.tag == "catSleep")
            {
                if (_player_animator.GetBool("BST") || jump) return;

                else
                {
                    SpriteRenderer catSleep = gameObject.GetComponent<SpriteRenderer>();
                    catSleep.sprite = catWakeUp;

                    if (MainMenu.AudioPlay)
                        gameObject.GetComponent<AudioSource>().Play();
                    BerryController.BumpOntheRoad = true;
                }
            }
            else if (gameObject.tag == "deer")
            {
                if (_player_animator.GetBool("BST")) return;

                else if (jump && deerSit) return;

                else
                {
                    if (MainMenu.AudioPlay)
                        gameObject.GetComponent<AudioSource>().Play();
                    gameObject.GetComponent<SpriteRenderer>().sprite = deerStand;
                    BerryController.BumpOntheRoad = true;
                }
            }
            else
            {
                if (_player_animator.GetBool("BST") || jump) return;

                else
                {
                    BerryController.BumpOntheRoad = true;
                    if (gameObject.tag == "puddle")
                    {
                        if (MainMenu.AudioPlay)
                            AudioManager.river.Play();
                    }
                    else return;
                }
            }
        }
        else if (gameObject.tag != "catMove")
        {
            if (collision.gameObject.tag != "Radar")
            {
                if (gameObject.tag == "river" && gameObject.transform.position.y>6) { bridge.SetActive(false); gameObject.SetActive(false); }
                else if (gameObject.tag == "Berry") { collision.gameObject.SetActive(false); }
                else { if (gameObject.transform.position.y > 6) gameObject.SetActive(false); }
            }
        }
        else if (gameObject.tag == "catMove")
            return;
    }
    IEnumerator deer()
    {
        while (true)
        {
            if (!GameManager.isPlay) yield return null;
            else
            {   
                GameObject target;
                if (transform.position.y < -8)
                {
                    gameObject.SetActive(false);
                    StopCoroutine(deer());
                }
                else
                {
                    if (!Input.GetMouseButtonDown(0)) yield return null;
                    else
                    {
                        Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (!Physics.Raycast(ray, out hit)) yield return null;
                        else
                        {
                            target = hit.collider.gameObject;
                            if (!target.Equals(gameObject)) yield return null;
                            else
                            {
                                gameObject.GetComponent<SpriteRenderer>().sprite = deerSitDown;
                                deerSit = true;
                                yield return null;
                            }
                        }
                    }
                }
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
        else
        {
            gameObject.transform.position = new Vector2(2.5f, -1);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        while (true)
        {
            if (!GameManager.isPlay) yield return null;
            else
            {
                if (Math.Abs(gameObject.transform.position.y) > 6.5f)
                {
                    gameObject.SetActive(false);
                    catBerry.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    break;
                }
                else
                {
                    if (dir == 0)
                        gameObject.transform.Translate(Vector2.right * Time.deltaTime * GameManager.gameSpeed * 8);

                    else
                        gameObject.transform.Translate(Vector2.left * Time.deltaTime * GameManager.gameSpeed * 8);
                    yield return null;
                }
            }
        }
        StopCoroutine(catRun());
    }
    IEnumerator BumpWithRiver(Collider collision, GameObject bridge)
    {
        while (true)
        {
            if (!GameManager.isPlay) yield return null;
            else
            {
                if (jump) break;
                else
                {
                    if (Math.Abs(collision.transform.position.x - bridge.transform.position.x) > 1f)
                    {
                        BerryController.BumpOntheRoad = true;
                        if (MainMenu.AudioPlay) AudioManager.river.Play();
                        break;
                    }
                    else if (bridge.transform.position.y < -5) break;
                    else yield return null;
                }
            }
        }
        StopCoroutine(BumpWithRiver(collision, bridge));
    }
}
