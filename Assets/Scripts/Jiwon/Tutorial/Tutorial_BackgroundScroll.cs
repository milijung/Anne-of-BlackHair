using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_BackgroundScroll : MonoBehaviour
{
    public int lowest;
    public int highst;
    private void OnEnable()
    {
        StartCoroutine("Move");
    }
    IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(Vector2.down * 1 * Time.deltaTime);

            if (transform.localPosition.y < lowest)
                transform.localPosition = new Vector2(0, highst);

            yield return null;
        }

    }
}
