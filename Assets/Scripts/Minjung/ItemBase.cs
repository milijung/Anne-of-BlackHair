using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public int type; // 0 = item_box , 1 = bleach , 2 = dye
    int LineNum;
    float posX;

    private void OnEnable() // 오브젝트가 활성화되면 실행
    {
        if (SpawnManager.MobStartNum == 0)
        {
            gameObject.SetActive(false); // SpawnManager 실행 전에 Mob이 등장하는 것 방지

        }
        else
        {
            gameObject.SetActive(true);
        }
        #region position X
        LineNum = (int)Random.Range(0, 3);
        if(LineNum == 0)
        {
            posX = -1.4f;
        }
        if (LineNum == 1)
        {
            posX = 0;
        }
        else
        {
            posX = 1.4f;
        }
        #endregion
        transform.position = new Vector2(posX,8);
    }

    private void Update()
    {
        if (GameManager.isPlay)
        {
            transform.Translate(Vector2.down * Time.deltaTime * GameManager.instance.gameSpeed * 12);
            if (transform.position.y < -8) // 화면 끝까지 Mob이 이동하면 해당 Mob 비활성화
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
            if(this.type == 0)
            {
                // ann_get_item_box
                Debug.Log("ann_get_item_box");
            }
            else if(this.type == 1)
            {
                // ann_get_bleach
                Debug.Log("ann_get_bleach");
            }
            else if(this.type == 2)
            {
                // ann_get_dye
                Debug.Log("ann_get_dye");
            }
        }
    }
}
