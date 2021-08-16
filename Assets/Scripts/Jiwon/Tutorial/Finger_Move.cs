using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger_Move : MonoBehaviour
{
    /*
    Vector2 startPosition = new Vector2(-1.56f, 2.34f);
    private void Awake()
    {
        transform.position = startPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(Finger_Move_1());
    }

    IEnumerator Finger_Move_1()
    {
        while (true)
        {
            transform.Translate(Vector2.right * 4);
            if (transform.position.x > 1.56f)
                transform.position = new Vector2(1.56f, transform.position.y);
            
            yield return new WaitForSeconds(0.9f);

            
            transform.Translate(Vector2.left * 4);
            if (transform.position.x < -1.56f)
            
            yield return new WaitForSeconds(0.9f);

            
            transform.position = new Vector2(1.56f, transform.position.y);
            transform.Translate(Vector2.left * 4);
            if (transform.position.x < -1.56f)
                transform.position = new Vector2(-1.56f, transform.position.y);
            
            yield return new WaitForSeconds(0.9f);

            
            transform.Translate(Vector2.right * 4);
            if (transform.position.x > 1.56f)
                transform.position = new Vector2(1.56f, transform.position.y);

            yield return new WaitForSeconds(0.9f);
            transform.position = new Vector2(-1.56f, transform.position.y);
        }
    }
    */
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * 0.2f);
    }
}
