using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public int type; // 0 = item_box , 1 = bleach , 2 = dye
    public int LineNum;
    float posX;
    public GameObject ItemAudio;
    AudioSource itemAudio;

    ItemController _item_controller;
    AnimationController _animation_controller;

    void Start()
    {
        itemAudio = ItemAudio.GetComponent<AudioSource>();
        _item_controller = GameObject.Find("Item_Controller").GetComponent<ItemController>();
        _animation_controller = GameObject.Find("Animation_Controller").GetComponent<AnimationController>();
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
            if (SpawnManager.isforest)
                posX = 0;
            else
            {
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
            }
            
        }
        transform.position = new Vector2(posX, 8);
    }

    private void Update()
    {
        if (GameManager.isPlay)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            
            if (transform.position.y < -8) // ȭ�� ������ Mob�� �̵��ϸ� �ش� Mob ��Ȱ��ȭ
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            if (MainMenu.AudioPlay)
            {
                itemAudio.Play();
            }
            if (this.type == 0)
            {
                // ann_get_item_box
                _item_controller._get_new_item_on_the_road();
            }
            if (this.type == 1)
            {
                // ann_get_bleach
                _animation_controller._ann_get_bleach();
            }
            if (this.type == 2)
            {
                // ann_get_dye
                _animation_controller._ann_get_dye();
            }
        }
        else if (collision.tag == "Radar")
        {
            collision.gameObject.SetActive(false);
        }
        else if(collision.tag == "Item")
        {
            if (gameObject.tag != "Item")
                collision.gameObject.SetActive(false);
        }
    }
}
