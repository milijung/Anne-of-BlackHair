using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Image : MonoBehaviour
{
    public GameObject SideMob_Controller;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public SideMob_Controller controller;

    private void OnEnable()
    {
        StartCoroutine(Image_Control());
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            controller.isSurprise = true;
        }
    }

    IEnumerator Image_Control()
    {
        while (true)
        {
            if (controller.isSurprise == true)
            {
                spriteRenderer.sprite = sprites[1];
                Debug.Log("왜 안될까");
            }
            else
            {
                spriteRenderer.sprite = sprites[0];
                Debug.Log("도대체가");
            }
            yield return null;
        }
    }
}
