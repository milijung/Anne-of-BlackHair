using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Move : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine("Move");
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (!spriteRenderer.flipX)
            {
                transform.Translate(Vector2.left * 7 * Time.deltaTime);
                if (transform.localPosition.x < -4.11)
                {
                    spriteRenderer.flipX = true;
                }
            }

            else
            {
                transform.Translate(Vector2.right * 7 * Time.deltaTime);
                if (transform.localPosition.x > 4.11)
                {
                    spriteRenderer.flipX = false;
                }
            }
            yield return null;
        }

    }
}
