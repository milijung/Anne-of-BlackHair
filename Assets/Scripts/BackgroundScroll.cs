using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    Material mat;
    float current_Y = 0;
    public float speed;
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material; 
    }

    void Update()
    {
        current_Y += speed * Time.deltaTime;
        mat.mainTextureOffset = new Vector2(0, current_Y);
    }
}
