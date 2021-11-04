using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    public GameObject toForest, toTown;
    public Sprite[] BackgroundImage;
    Material mat; // Material �ν��Ͻ�
    float current_Y = 0; // ��� �̹����� Y��ǥ

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        StartCoroutine(chooseIMG());
    }

    void Update()
    {
        if (GameManager.isPlay)
        {
            current_Y += GameManager.gameSpeed * Time.deltaTime;
            mat.mainTextureOffset = new Vector2(0, current_Y);
        }
    }
    IEnumerator chooseIMG()
    {
        while (true)
        {

            if (GameManager.isPlay)
            {
                if (SpawnManager.isforest)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = BackgroundImage[0]; // ����
                    while (SpawnManager.isforest || GameManager.speedIndex == 3)
                    {
                        SpawnManager.isforest = true;
                        yield return null;
                    }
                }
                else if(!SpawnManager.isforest)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = BackgroundImage[1]; // ����
                    while (!SpawnManager.isforest || GameManager.speedIndex == 3)
                    {
                        SpawnManager.isforest = false;
                        yield return null;
                    }
                    yield return new WaitForSeconds(2f);
                }
                else {}
            }
            yield return null;
        }
    }
}
