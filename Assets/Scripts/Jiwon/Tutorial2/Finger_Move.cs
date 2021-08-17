using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger_Move : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Control());
    }
    private void Move_Right()
    {
        transform.Translate(Vector2.right * 0.5f);
    }

    private void Move_Left()
    {
        transform.Translate(Vector2.left * 0.5f);
    }
    IEnumerator Control()
    {
        while (true)
        {
            Move_Right();
            if (transform.position.x > 1.56)
                transform.position = new Vector2(1.56f, transform.position.y);
            yield return new WaitForSeconds(1f);
            Move_Left();
            if (transform.position.x < -1.56)
                transform.position = new Vector2(-1.56f, transform.position.y);
            yield return new WaitForSeconds(0.5f);
            transform.position = new Vector2(1.56f, transform.position.y);
            yield return new WaitForSeconds(0.5f);
            Move_Left();
            if (transform.position.x < -1.56)
                transform.position = new Vector2(-1.56f, transform.position.y);
            yield return new WaitForSeconds(1f);
            Move_Right();
            if (transform.position.x > 1.56)
                transform.position = new Vector2(1.56f, transform.position.y);
            yield return new WaitForSeconds(1f);
            transform.position = new Vector2(-1.56f, transform.position.y);
        }
    }
}
