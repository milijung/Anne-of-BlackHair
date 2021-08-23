using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_change : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    public float waitSeconds;
    int i;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(waitSeconds);
            if (i < sprites.Length - 1)
                i++;
            else
                i = 0;
        }
    }

}
