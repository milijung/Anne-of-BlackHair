using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emergency_Text : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("Move");
    }
    IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(Vector2.left * 8.3f * Time.deltaTime);

            if (transform.localPosition.x < -687)
                transform.localPosition = new Vector2(725, 314);

            yield return null;
        }

    }
}
