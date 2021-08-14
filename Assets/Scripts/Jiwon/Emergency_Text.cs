using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emergency_Text : MonoBehaviour
{
    public int textSpeed;
    //private RectTransform rectTransform;

    private void Awake()
    {

        //rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-textSpeed, 0, 0);
        //rectTransform.position = new Vector2(textSpeed*(-1), rectTransform.position.y);
    }
}
