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
            transform.Translate(Vector2.left * 9 * Time.deltaTime);

            if (transform.localPosition.x < -801)
                transform.localPosition = new Vector2(801, 344);

            yield return null;
        }
        
    }
}
