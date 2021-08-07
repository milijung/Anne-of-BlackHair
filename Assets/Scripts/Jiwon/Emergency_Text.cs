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
            transform.Translate(Vector2.left * 7 * Time.deltaTime);

            if (transform.localPosition.x <= -565)
                transform.localPosition = new Vector2(774, 380);
            yield return null;
        }
    }
}
