using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBase : MonoBehaviour
{
    public Vector2 StartPosition;
    public GameObject way1, way2;
    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�Ǹ� ����
    {
        
        if (SpawnManager.MobStartNum ==0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (way1 != null && way2 != null) { way1.SetActive(true); way2.SetActive(true); }
            transform.position = StartPosition;
            gameObject.SetActive(true);
            
        }
        
    }

    private void Update()
    {
        if (gameObject.tag != "way")
        {
            if (GameManager.isPlay)
            {
                transform.Translate(Vector2.down * Time.deltaTime * GameManager.gameSpeed * 12);
                if (gameObject.tag == "toTown" || gameObject.tag == "toForest")
                {
                    if (transform.position.y < -20)
                    {
                        gameObject.SetActive(false);
                        way1.SetActive(false);
                        way2.SetActive(false);
                    }
                }
                else
                {
                    if (transform.position.y < -8)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}