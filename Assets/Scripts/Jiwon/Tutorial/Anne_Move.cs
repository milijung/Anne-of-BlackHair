using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anne_Move : MonoBehaviour
{
    Vector2 startPosition = new Vector2(0, 1.09f);
    private void Awake()
    {
        transform.position = startPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(Ann_Move());
    }

    IEnumerator Ann_Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            transform.Translate(Vector2.right * 7);
            if (transform.position.x > 0.98f)
                transform.position = new Vector2(0.98f, transform.position.y);
            yield return new WaitForSeconds(1f);

            transform.Translate(Vector2.left * 7);
            if (transform.position.x < 0)
                transform.position = new Vector2(0, transform.position.y);
            yield return new WaitForSeconds(1f);

            transform.Translate(Vector2.left * 7);
            if (transform.position.x < -0.94f)
                transform.position = new Vector2(-0.94f, transform.position.y);
            yield return new WaitForSeconds(1f);

            transform.Translate(Vector2.right * 7);
            if (transform.position.x > 0)
                transform.position = new Vector2(0, transform.position.y);
            
        }
    }
}
