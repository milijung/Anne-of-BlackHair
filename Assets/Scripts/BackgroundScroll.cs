using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    Material mat; // Material 인스턴스
    float current_Y = 0; // 배경 이미지의 Y좌표

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material; 
    }

    void Update()
    {
        current_Y += GameManager.instance.gameSpeed * Time.deltaTime;
        mat.mainTextureOffset = new Vector2(0, current_Y);
    }
}
