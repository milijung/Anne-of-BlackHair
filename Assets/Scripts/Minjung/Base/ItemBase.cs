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

    GameObject _player;
    GameObject _twinkle;

    ItemController _item_controller;

    void Start()
    {
        _player = GameObject.Find("Player");
        _twinkle = GameObject.Find("Twinkle");

        itemAudio = ItemAudio.GetComponent<AudioSource>();
        _item_controller = GameObject.Find("Item_Controller").GetComponent<ItemController>();   
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
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.gameSpeed * 12);
            
            if (transform.position.y < -8) // ȭ�� ������ Mob�� �̵��ϸ� �ش� Mob ��Ȱ��ȭ
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Animator _player_animator = _player.GetComponent<Animator>();

        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);

            if (MainMenu.AudioPlay) itemAudio.Play();

            if (this.type == 0)
            {
                // ann_get_item_box
                _item_controller._get_new_item_on_the_road();
            }
            if (this.type == 1)
            {
                // ann_get_bleach
                if (_player_animator.GetInteger("State") <= 5) _player_animator.SetInteger("State", 5);
            }
            if (this.type == 2)
            {
                // ann_get_dye
                if (_player_animator.GetInteger("State") >= 3) {

                    // twinkle on
                    _twinkle.GetComponent<Animator>().SetBool("T", true);

                    _player_animator.SetBool("RED", true);
                    _player_animator.SetInteger("State", 9);
                }
            }
        }
        else
        {
            if (collision.tag == "Color" || collision.tag== "Road") 
            {
                if(gameObject.transform.position.y >6)
                    collision.gameObject.SetActive(false);
            }
            if(collision.tag == "Berry") { gameObject.SetActive(false); }
        }
    }
}
