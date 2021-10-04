using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryController : MonoBehaviour
{
    public GameObject[] Berry_play;
    public GameObject player;
    public TextMeshProUGUI BerryText;
    public static int BerryNum;
    public static bool getBerryBox, getBerry, BumpOntheRoad, BumpWithCat= false;

    Animator _lip_move_animator;

    private void Start()
    {
        BerryNum = 0; 

        for (int i = 0; i < Berry_play.Length; i++)
            Berry_play[i].SetActive(false);
        BerryText.gameObject.SetActive(false);
        getBerryBox = false;
        getBerry = false;
        BumpOntheRoad = false;
        RoadBase.jump = false;
        StartCoroutine(BerryStart());
        StartCoroutine(BerryChange());

        
    }
    private void Update()
    {
        if (!getBerryBox) return;
        else
        {
            if (GameManager.isPlay && BerryNum == 0)
            {
                for (int i = 0; i < Berry_play.Length; i++)
                    Berry_play[i].SetActive(false);

                BerryText.gameObject.SetActive(false);
                GameManager.instance.GameOver();
            }
            else if (BerryNum == 2)
            {
                Berry_play[1].SetActive(false);
                BerryText.gameObject.SetActive(false);
                Berry_play[2].SetActive(true);
            }
            else if (BerryNum == 1)
            {
                Berry_play[2].SetActive(false);
            }
            else
            {
                Berry_play[1].SetActive(true);
                BerryText.gameObject.SetActive(true);
                Berry_play[2].SetActive(false);
                BerryText.text = "<color=#000000>" + BerryNum.ToString() + "</color>";
            }
        }
    }
    // Update is called once per frame
    public IEnumerator BerryStart()
    {
        while (true)
        {
            if (!getBerryBox) yield return null;
            else
            {
                for (int i = 0; i < 2; i++)
                    Berry_play[i].SetActive(true);
                BerryText.gameObject.SetActive(true);
                break;
            }
        }
        StopCoroutine(BerryStart());
    }
    public IEnumerator BerryChange()
    {
        while (true)
        {
            if (getBerry)
            {
                BerryNum++;
                yield return new WaitForSeconds(0.1f);
                getBerry = false;
                yield return null;
            }
            else if (!BumpOntheRoad) yield return null;
            else
            {
                BerryNum--;
                Berry_play[3].transform.position = new Vector2(player.transform.position.x + 0.2f, -4.5f);
                if (!BumpWithCat)
                    StartCoroutine(BerryDrop());
                
                yield return new WaitForSeconds(0.1f);
                BumpOntheRoad = BumpWithCat = false;
            }
        }
    }
    public IEnumerator BerryDrop()
    {
        if(MainMenu.AudioPlay) GameObject.Find("BerryDropAudio").GetComponent<AudioSource>().Play();
        Berry_play[3].SetActive(true);
        while (true)
        {
            if (Berry_play[3].transform.position.y > -5.5f)
            {
                Berry_play[3].transform.Translate(Vector2.down * Time.deltaTime * GameManager.gameSpeed * 7);
                yield return null;
            }
            else
            {
                Berry_play[3].SetActive(false);
                break;
            } 
        }
        StopCoroutine(BerryDrop());
    }
}
