using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_change : MonoBehaviour
{
    public Sprite[] sprites;
    //SpriteRenderer spriteRenderer;
    Image my_img;
    public float waitSeconds;
    int i;
    void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        my_img = GetComponent<Image>();
        i = 0;

    }

    private void OnEnable()
    {
        StartCoroutine(ChangeCats());
    }

    IEnumerator ChangeCats()
    {
        while (true)
        {
            //spriteRenderer.sprite = sprites[i];
            my_img.sprite = sprites[i];
            yield return new WaitForSeconds(waitSeconds);
            if (i < sprites.Length - 1)
                i++;
            else
                i = 0;
        }
    }

}
