using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    public GameObject toForest, toTown;
    public Sprite[] BackgroundImage;
    Material mat; // Material 인스턴스
    float current_Y = 0; // 배경 이미지의 Y좌표

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        StartCoroutine(chooseIMG());
    }

    void Update()
    {
        if (GameManager.isPlay)
        {
            current_Y += GameManager.instance.gameSpeed * Time.deltaTime;
            mat.mainTextureOffset = new Vector2(0, current_Y);
        }
    }
    IEnumerator chooseIMG()
    {
        while (true)
        {

            if (GameManager.isPlay)
            {
                if (SpawnManager.isforest && toForest.transform.position.y <= 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = BackgroundImage[0]; // 한줄
                    while (SpawnManager.isforest)
                    {
                        yield return null;
                    }
                }
                else if(!SpawnManager.isforest && toTown.transform.position.y<=0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = BackgroundImage[1]; // 세줄
                    while (!SpawnManager.isforest)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(2f);
                }
            }
            yield return null;
        }
    }
}
