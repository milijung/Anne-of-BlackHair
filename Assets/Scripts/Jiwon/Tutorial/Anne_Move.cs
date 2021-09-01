using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anne_Move : MonoBehaviour
{
    //Vector2 startPosition = new Vector2(0, 183);
    private void Awake()
    {
        //transform.position = startPosition;
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
            transform.Translate(Vector2.right * 5);
            if (transform.position.x > 0.7f)
                transform.position = new Vector2(0.7f, transform.position.y);
            yield return new WaitForSeconds(1f);

            transform.Translate(Vector2.left * 5);
            if (transform.position.x < 0)
                transform.position = new Vector2(0, transform.position.y);
            yield return new WaitForSeconds(1f);

            transform.Translate(Vector2.left * 5);
            if (transform.position.x < -0.7f)
                transform.position = new Vector2(-0.7f, transform.position.y);
            yield return new WaitForSeconds(1f);

            transform.Translate(Vector2.right * 5);
            if (transform.position.x > 0)
                transform.position = new Vector2(0, transform.position.y);
            
        }
    }
}
